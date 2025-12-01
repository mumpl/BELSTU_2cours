using DAL_LES;
using ServiceLocator;

namespace Test_SL_LES
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Init.Execute();

            //Создание и настройка ServiceLocator
            var serviceLocator = new ServiceLocator.ServiceLocator();

            // Регистрация основных сервисов
            serviceLocator.Register<IRepository>(() => Repository.Create(), ServiceLifetime.Transient);
            serviceLocator.Register<ICelebrity>(() => Repository.Create(), ServiceLifetime.Transient);
            serviceLocator.Register<ILifeevent>(() => Repository.Create(), ServiceLifetime.Transient);
            serviceLocator.Register<ICommon>(() => Repository.Create(), ServiceLifetime.Transient);

            // Регистрация сервисов для кэширования
            serviceLocator.Register<ICashCelebrities>(() =>
                new CashCelebrities(serviceLocator.GetService<IRepository>()),
                ServiceLifetime.Scoped);

            serviceLocator.Register<ICashService>(() =>
                serviceLocator.GetService<ICashCelebrities>(),
                ServiceLifetime.Scoped);

            
            Console.WriteLine("*** Тестирование Transient-сервисов ***");

            TestTransientServices(serviceLocator);

            
            Console.WriteLine("\n*** Операции с данными ***");
            TestDataOperations(serviceLocator);

            
            Console.WriteLine("\n*** Тестирование кэширования ***");
            TestCaching(serviceLocator);

            
            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
            serviceLocator.Dispose();
        }

        static void TestTransientServices(ServiceLocator.ServiceLocator serviceLocator)
        {
            var celebrityService1 = serviceLocator.GetService<ICelebrity>();
            var celebrityService2 = serviceLocator.GetService<ICelebrity>();
            Console.WriteLine($"Одинаковые экземпляры ICelebrity? {ReferenceEquals(celebrityService1, celebrityService2)}");

            var lifeeventService1 = serviceLocator.GetService<ILifeevent>();
            var lifeeventService2 = serviceLocator.GetService<ILifeevent>();
            Console.WriteLine($"Одинаковые экземпляры ILifeevent? {ReferenceEquals(lifeeventService1, lifeeventService2)}");

            var commonService1 = serviceLocator.GetService<ICommon>();
            var commonService2 = serviceLocator.GetService<ICommon>();
            Console.WriteLine($"Одинаковые экземпляры ICommon? {ReferenceEquals(commonService1, commonService2)}");

            var repositoryService1 = serviceLocator.GetService<IRepository>();
            var repositoryService2 = serviceLocator.GetService<IRepository>();
            Console.WriteLine($"Одинаковые экземпляры IRepository? {ReferenceEquals(repositoryService1, repositoryService2)}");
        }

        static void TestDataOperations(ServiceLocator.ServiceLocator serviceLocator)
        {
            var repository = serviceLocator.GetService<IRepository>();

            // Добавляем знаменитость
            var newCelebrity = new Celebrity { FullName = "Enthony Hopkins", Nationality = "US" };
            repository.AddCelebrity(newCelebrity);
            Console.WriteLine("Добавлена новая знаменитость: Энтони Хопкинс");

            
            var celebrities = repository.GetAllCelebrities();
            Console.WriteLine("\nВсе знаменитости:");
            foreach (var c in celebrities)
            {
                Console.WriteLine($"{c.Id}: {c.FullName} ({c.Nationality})");
            }

            // Добавляем событие
            if (celebrities.Count > 0)
            {
                var newEvent = new Lifeevent
                {
                    CelebrityId = celebrities[0].Id,
                    Date = DateTime.Now,
                    Description = "Получил премию Оскар"
                };
                repository.AddLifeevent(newEvent);
                Console.WriteLine("\nДобавлено новое событие для знаменитости");

                // Выводим события
                var events = repository.GetLifeeventsByCelebrityId(celebrities[0].Id);
                Console.WriteLine("\nСобытия в жизни знаменитости:");
                foreach (var e in events)
                {
                    Console.WriteLine($"{e.Date.ToShortDateString()}: {e.Description}");
                }
            }
        }

        static void TestCaching(ServiceLocator.ServiceLocator serviceLocator)
        {
            using (var scope = serviceLocator.CreateScope())
            {
                try
                {
                    var cashService = scope.ServiceLocator.GetService<ICashCelebrities>();
                    cashService.CacheDuration = TimeSpan.FromSeconds(10);

                    // Первое получение - загрузка данных
                    var celebrities = cashService.GetCachedCelebrities();
                    Console.WriteLine($"Первая загрузка: {celebrities.Count} знаменитостей, время: {cashService.LastUpdateTime}");

                    // Второе получение - из кэша
                    celebrities = cashService.GetCachedCelebrities();
                    Console.WriteLine($"Второе получение: {celebrities.Count} знаменитостей, время: {cashService.LastUpdateTime}");

                    // Принудительное обновление
                    Console.WriteLine("\nПринудительное обновление кэша...");
                    cashService.ResetCache(); // Используем напрямую ICashCelebrities

                    celebrities = cashService.GetCachedCelebrities();
                    Console.WriteLine($"После обновления: {cashService.LastUpdateTime}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при тестировании кэширования: {ex.Message}");
                }
            }
        }
    }
}
