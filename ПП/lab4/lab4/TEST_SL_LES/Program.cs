using DAL_LES;
using Microsoft.Extensions.DependencyInjection;
using ServiceLocator;

class Program
{
    static void Main()
    {

        TransientLocator.Register<IRepository>(() => new Repository());                //новый экз репозитори при каждом запросе
        TransientLocator.Register<ICelebrity>(() => new Repository());
        TransientLocator.Register<ILifeevent>(() => new Repository());
        TransientLocator.Register<ICommon>(() => new Repository());

        Console.WriteLine("*** TransientLocator ***\n");

        int celebId;
        int eventId;

        // Тест ICelebrity
        using (ICelebrity celebrityService = TransientLocator.Resolve<ICelebrity>())
        {
            Console.WriteLine("*** Добавление Celebrity ***");

            Celebrity newCelebrity = new Celebrity
            {
                FullName = "Эдриен Броуди",
                Nationality = "US",
            };

            celebrityService.AddCelebrity(newCelebrity);

            List<Celebrity> celebrities = celebrityService.GetAllCelebrities();
            foreach (Celebrity c in celebrities)
            {
                Console.WriteLine($"Celebrity: {c.Id}: {c.FullName}, {c.Nationality}");
            }

            celebId = celebrities.First().Id;

            Celebrity getCelebrity = celebrityService.GetCelebrityById(celebId);
            Console.WriteLine($"Получение селебрити по ID: {getCelebrity.FullName}");

            Celebrity updated = new Celebrity
            {
                FullName = "Эдриен Николас Броуди",
                Nationality = "US",
            };

            celebrityService.UpdCelebrity(celebId, updated);
            Console.WriteLine("Данные обновлены успешно!");
        }

        Console.WriteLine();

        // Тест ILifeevent
        using (ILifeevent lifeeventService = TransientLocator.Resolve<ILifeevent>())
        {
            Console.WriteLine("*** Добавление события ***");

            Lifeevent lifeevent = new Lifeevent
            {
                CelebrityId = celebId,
                Date = new DateTime(1952, 1, 1),
                Description = "Получил оскар за лучшую мужскую роль в 2003 году",
            };

            lifeeventService.AddLifeevent(lifeevent);

            List<Lifeevent> events = lifeeventService.GetAllLifeevents();
            foreach (Lifeevent e in events)
            {
                Console.WriteLine($"Lifeevent: {e.Id}: {e.Date.ToShortDateString()} — {e.Description}");
            }

            eventId = events.First().Id;

            Lifeevent getEvent = lifeeventService.GetLifeeventById(eventId);
            Console.WriteLine($"Загружено событие: {getEvent.Description}");

            Lifeevent updatedEvent = new Lifeevent
            {
                Id = eventId,
                CelebrityId = celebId,
                Date = new DateTime(1954, 1, 1),
                Description = "Получил оскар за лучшую мужскую роль в 2003 и 2025 годах",
            };

            lifeeventService.UpdLifeevent(eventId, updatedEvent);
            Console.WriteLine("Событие обновлено успешно!");
        }

        Console.WriteLine();

        // Тест ICommon
        using (ICommon commonService = TransientLocator.Resolve<ICommon>())
        {
            Console.WriteLine("ICommon (связи между таблицами)");

            List<Lifeevent> events = commonService.GetLifeeventsByCelebrityId(celebId);
            foreach (Lifeevent e in events)
            {
                Console.WriteLine($"ByCelebrityId: {e.Description}");
            }

            Celebrity celebrity = commonService.GetCelebrityByLifeeventId(eventId);
            Console.WriteLine($"ByEventId: cобытие принадлежит: {celebrity.FullName}");
        }

        Console.WriteLine();

        // IRepository удаление 
        using (IRepository repo = TransientLocator.Resolve<IRepository>())
        {
            Console.WriteLine("*** Удаление IRepository ***");

            repo.DelLifeevent(eventId);
            Console.WriteLine("Событие удалено успешно!");

            repo.DelCelebrity(celebId);
            Console.WriteLine("Знаменитость удалена успешно!");
        }

        Console.WriteLine("\n*** Тест через ScopeLocator ***");

        // Регистрируем Scoped-сервисы
        ScopeLocator.Register<IRepository>(sp => new Repository());
        ScopeLocator.Register<ICacheCelebrities>(sp =>
        {
            IRepository repo = sp.GetService<IRepository>();
            return new CacheCelebrities(repo, TimeSpan.FromSeconds(5));
        });

        IServiceScopeFactory scopeFactory = ScopeLocator.CreateServiceScopeFactory();

        // Первый Scope 
        Console.WriteLine("\nScope 1");
        using (IServiceScope scope1 = scopeFactory.CreateScope())
        {
            ICacheCelebrities cache1 = scope1.ServiceProvider.GetService<ICacheCelebrities>();

            Console.WriteLine("Первый вызов:");
            List<Celebrity> result1 = cache1.GetCachedCelebrities();
            Console.WriteLine($"Кол-во: {result1.Count}");

            Console.WriteLine("Второй вызов через 2 сек (использует кэш):");
            Thread.Sleep(2000);
            List<Celebrity> result2 = cache1.GetCachedCelebrities();
            Console.WriteLine($"Кол-во: {result2.Count}");

            Console.WriteLine("Третий вызов через 5 сек (обновленный кэш):");
            Thread.Sleep(5000);
            List<Celebrity> result3 = cache1.GetCachedCelebrities();
            Console.WriteLine($"Кол-во: {result3.Count}");
        }

        // Второй Scope
        Console.WriteLine("\nScope 2 — новый объект кэша");
        using (IServiceScope scope2 = scopeFactory.CreateScope())
        {
            ICacheCelebrities cache2 = scope2.ServiceProvider.GetService<ICacheCelebrities>();

            Console.WriteLine("Первый вызов (новый кэш):");
            List<Celebrity> result4 = cache2.GetCachedCelebrities();
            Console.WriteLine($"Кол-во: {result4.Count}");
        }

    }
}

