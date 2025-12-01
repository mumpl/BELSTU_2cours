using DAL_LES;
class Program
{
    static void Main()
    {
        using (IRepository repo = new Repository())
        {
            Console.WriteLine("*** Добавление знаменитости ***");
            Celebrity newCelebrity = new Celebrity
            {
                FullName = "Эдриен Броуди",
                Nationality = "US",
            };

            repo.AddCelebrity(newCelebrity);
            Console.WriteLine($"Добавлено: {newCelebrity.FullName}\n");

            Console.WriteLine("*** Все знаменитости ***");
            List<Celebrity> celebrities = repo.GetAllCelebrities();
            foreach (Celebrity c in celebrities)
            {
                Console.WriteLine($"Id: {c.Id}, FullName: {c.FullName}, Nationality: {c.Nationality}");
            }

            int celebId = celebrities.First().Id;

            Console.WriteLine("\n*** Получение знаменитости по id ***");
            Celebrity? loaded = repo.GetCelebrityById(celebId);
            Console.WriteLine($"Знаменитость по Id {celebId}: {loaded.FullName}");

            Console.WriteLine("\n*** Обновление знаменитости ***");
            Celebrity updated = new Celebrity
            {
                FullName = "Эдриен Николас Броуди",
                Nationality = "US",
            };
            repo.UpdCelebrity(celebId, updated);
            Console.WriteLine("Знаменитость обновлена успешно!");

            Console.WriteLine("\n*** Добавление события ***");
            Lifeevent newEvent = new Lifeevent
            {
                CelebrityId = celebId,
                Date = new DateTime(1936, 1, 1),
                Description = "Получил оскар за лучшую мужскую роль в 2003 году",
            };

            repo.AddLifeevent(newEvent);
            Console.WriteLine($"Добавлено событие: {newEvent.Description}\n");

            Console.WriteLine("*** Получение всех событий ***");
            List<Lifeevent> events = repo.GetAllLifeevents();
            foreach (Lifeevent e in events)
            {
                Console.WriteLine($"Id: {e.Id}, Date: {e.Date.ToShortDateString()}, Desc: {e.Description}");
            }

            int eventId = events.First().Id;

            Console.WriteLine("\n*** Получение события по id ***");
            Lifeevent? loadedEvent = repo.GetLifeeventById(eventId);
            Console.WriteLine($"Событие по Id {eventId}: {loadedEvent.Description}");

            Console.WriteLine("\n*** Обновление события ***");
            Lifeevent updatedEvent = new Lifeevent
            {
                Id = eventId,
                CelebrityId = celebId,
                Date = new DateTime(1945, 1, 1),
                Description = "Получил оскар за лучшую мужскую роль в 2003 и 2025 годах",

            };

            repo.UpdLifeevent(eventId, updatedEvent);
            Console.WriteLine("Событие обновлено успешно!");

            Console.WriteLine("\n*** Получение знаменитости по id события ***");
            Celebrity? fromEvent = repo.GetCelebrityByLifeeventId(eventId);
            Console.WriteLine($"Знаменитость по событию {eventId}: {fromEvent.FullName}");

            Console.WriteLine("\n*** Удаление события ***");
            repo.DelLifeevent(eventId);
            Console.WriteLine("Событие удалено успешно!");

            Console.WriteLine("\n*** Удаление знаменитости ***");
            repo.DelCelebrity(celebId);
            Console.WriteLine("Знаменитость удалена успешно!");
        }
    }
}

