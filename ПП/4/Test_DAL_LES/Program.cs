using DAL_LES;
using DAL_LES.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("=== Init Test ===");

        using (IRepository repo = Repository.Create())
        {
            Console.WriteLine("\n\n[---IRepository---]");
            Console.WriteLine("\n--- AddCelebrity ---");
            var celeb1 = new Celebrity { FullName = "Albert Einstein", Nationality = "German" };
            var celeb2 = new Celebrity { FullName = "Marie Curie", Nationality = "Polish" };
            repo.AddCelebrity(celeb1);
            repo.AddCelebrity(celeb2);
            Console.WriteLine("Added Celebrities");

            Console.WriteLine("\n--- GetAllCelebrities ---");
            var celebrities = repo.GetAllCelebrities();
            celebrities.ForEach(c => Console.WriteLine($"Celebrity {c.Id}: {c.FullName}, {c.Nationality}"));

            Console.WriteLine("\n--- GetCelebrityById ---");
            var singleCeleb = repo.GetCelebrityById(celebrities[1].Id);
            Console.WriteLine($"Celebrity by Id: {singleCeleb?.FullName}");

            Console.WriteLine("\n--- DelCelebrity ---");
            if (repo.DelCelebrity(celeb2.Id)) Console.WriteLine("Deleted Celebrity");

            Console.WriteLine("\n--- UpdCelebrity ---");
            var celebToUpdate = repo.GetCelebrityById(celebrities[0].Id);
            if (celebToUpdate != null)
            {
                celebToUpdate.Nationality = "German-American";
                if (repo.UpdCelebrity(celebToUpdate.Id, celebToUpdate))
                    Console.WriteLine($"Updated celebrity: {celebToUpdate.FullName}, new nationality: {celebToUpdate.Nationality}");
                else
                    Console.WriteLine("Update failed");
            }


            Console.WriteLine("\n--- AddLifeEvents ---");
            var event1 = new LifeEvent
            {
                CelebrityId = celebrities[0].Id,
                Description = "Born in Ulm",
                Date = new DateTime(1879, 3, 14)
            };
            var event2 = new LifeEvent
            {
                CelebrityId = celebrities[1].Id,
                Description = "Won Nobel Prize",
                Date = new DateTime(1903, 12, 10)
            };

            repo.AddLifeEvent(event1);
            repo.AddLifeEvent(event2);
            Console.WriteLine("Added LifeEvents");

            Console.WriteLine("\n--- GetAllLifeEvents ---");
            var events = repo.GetAllLifeEvents();
            events.ForEach(e => Console.WriteLine($"LifeEvent {e.Id}: {e.Description}, {e.Date.ToShortDateString()}, CelebrityId: {e.CelebrityId}"));

            Console.WriteLine("\n--- UpdLifeEvent ---");
            var firstEvent = events.First();
            firstEvent.Description = "Born in Germany";
            if (repo.UpdLifeEvent(firstEvent.Id, firstEvent))
                Console.WriteLine("LifeEvent updated");
            else
                Console.WriteLine("LifeEvent update failed");

            Console.WriteLine("\n--- GetAllLifeEvents ---");
            repo.GetAllLifeEvents().ForEach(e => Console.WriteLine($"LifeEvent {e.Id}: {e.Description}"));

            Console.WriteLine("\n--- DelLifeEvent ---");
            if (repo.DelLifeEvent(event2.Id)) Console.WriteLine("Deleted LifeEvent");


            Console.WriteLine("\n--- GetLifeEventsByCelebrityId ---");
            var celeb1Events = repo.GetLifeEventsByCelebrityId(celebrities[0].Id);
            celeb1Events.ForEach(e => Console.WriteLine($"Event for {celebrities[0].FullName}: {e.Description}"));

            Console.WriteLine("\n--- GetCelebrityByLifeEventId ---");
            if (celeb1Events.Count > 0)
            {
                var celebrityForEvent = repo.GetCelebrityByLifeEventId(celeb1Events[0].Id);
                Console.WriteLine($"Celebrity for event '{celeb1Events[0].Description}': {celebrityForEvent.FullName}");
            }
            else
            {
                Console.WriteLine("No events available to test GetCelebrityByLifeEventId");
            }

            repo.Dispose();
        }

        Console.WriteLine("=== Test Complete ===");
        Console.ReadLine();
    }
}
