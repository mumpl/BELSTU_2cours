using DAL_LES;

namespace Test_DAL_LES
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*** ТЕСТИРОВАНИЕ РЕПОЗИТОРИЯ DAL_LES ***");

            
            DAL_LES.Init.Execute();

            using (var repo = Repository.Create())
            {
                TestCelebrityOperations(repo);
                TestLifeeventOperations(repo);
                TestCommonOperations(repo);
            }

            Console.WriteLine("\nТестирование завершено. Нажмите любую клавишу...");
            Console.ReadKey();
        }

        static void TestCelebrityOperations(IRepository repo)
        {
            Console.WriteLine("\n*** ТЕСТИРОВАНИЕ ОПЕРАЦИЙ С ЗНАМЕНИТОСТЯМИ ***");

            // 1. Добавление знаменитости
            var newCeleb = new Celebrity
            {
                FullName = "Энтони Хопкинс",
                Nationality = "US"
            };
            Console.WriteLine($"Добавление знаменитости: {newCeleb.FullName}");
            Console.WriteLine($"Результат: {repo.AddCelebrity(newCeleb)}");

            // 2. Получение всех знаменитостей
            var celebs = repo.GetAllCelebrities();
            Console.WriteLine($"\nСписок знаменитостей ({celebs.Count}):");
            celebs.ForEach(c => Console.WriteLine($"{c.Id}: {c.FullName} ({c.Nationality})"));

            // 3. Обновление знаменитости
            if (celebs.Count > 0)
            {
                var celebToUpdate = celebs[0];
                celebToUpdate.Nationality = "США";
                Console.WriteLine($"\nОбновление знаменитости ID {celebToUpdate.Id}");
                Console.WriteLine($"Результат: {repo.UpdCelebrity(celebToUpdate.Id, celebToUpdate)}");
            }

            // 4. Удаление знаменитости
            if (celebs.Count > 1)
            {
                Console.WriteLine($"\nУдаление знаменитости ID {celebs[1].Id}");
                Console.WriteLine($"Результат: {repo.DelCelebrity(celebs[1].Id)}");
            }
        }

        static void TestLifeeventOperations(IRepository repo)
        {
            Console.WriteLine("\n*** ТЕСТИРОВАНИЕ ОПЕРАЦИЙ СОБЫТИЙ ***");

            // Получаем первую знаменитость
            var celebrity = repo.GetAllCelebrities().FirstOrDefault();
            if (celebrity == null)
            {
                Console.WriteLine("Нет знаменитостей для тестирования событий");
                return;
            }

            // 1. Добавление события
            var newEvent = new Lifeevent
            {
                CelebrityId = celebrity.Id,
                Date = new DateTime(1994, 7, 6),
                Description = "Премьера фильма 'Молчание ягнят'"
            };
            Console.WriteLine($"Добавление события: {newEvent.Description}");
            Console.WriteLine($"Результат: {repo.AddLifeevent(newEvent)}");

            // 2. Получение всех событий
            var events = repo.GetAllLifeevents();
            Console.WriteLine($"\nСписок событий ({events.Count}):");
            events.ForEach(e => Console.WriteLine($"{e.Id}: {e.Date:d} - {e.Description}"));

            // 3. Обновление события
            if (events.Count > 0)
            {
                var eventToUpdate = events[0];
                eventToUpdate.Description += " (Оскар за лучшую мужскую роль)";
                Console.WriteLine($"\nОбновление события ID {eventToUpdate.Id}");
                Console.WriteLine($"Результат: {repo.UpdLifeevent(eventToUpdate.Id, eventToUpdate)}");
            }

            // 4. Удаление события
            if (events.Count > 1)
            {
                Console.WriteLine($"\nУдаление события ID {events[1].Id}");
                Console.WriteLine($"Результат: {repo.DelLifeevent(events[1].Id)}");
            }
        }

        static void TestCommonOperations(IRepository repo)
        {
            Console.WriteLine("\n*** ТЕСТИРОВАНИЕ ОБЩИХ МЕТОДОВ ***");

            // 1. Получение событий по ID знаменитости
            var celebrity = repo.GetAllCelebrities().FirstOrDefault();
            if (celebrity != null)
            {
                var celebrityEvents = repo.GetLifeeventsByCelebrityId(celebrity.Id);
                Console.WriteLine($"\nСобытия знаменитости {celebrity.FullName} ({celebrityEvents.Count}):");
                celebrityEvents.ForEach(e => Console.WriteLine($"- {e.Description} ({e.Date:d})"));
            }

            // 2. Получение знаменитости по ID события
            var lifeevent = repo.GetAllLifeevents().FirstOrDefault();
            if (lifeevent != null)
            {
                var eventCelebrity = repo.GetCelebrityByLifeeventId(lifeevent.Id);
                Console.WriteLine($"\nЗнаменитость для события '{lifeevent.Description}':");
                Console.WriteLine(eventCelebrity != null
                    ? $"{eventCelebrity.FullName} ({eventCelebrity.Nationality})"
                    : "Не найдена");
            }
        }
    }
}