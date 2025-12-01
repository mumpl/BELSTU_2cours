using REPO;
using DALJSONX;

namespace TEST_DALJSONX
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            const string fileName = "WSRef.json";
            Console.WriteLine("Init");

            InitializeData(fileName);

            Console.WriteLine("Start");
            using (IRepository repo = Repository.Create(fileName))
            {
                // Test GetAllWSRef
                repo.GetAllWSRef().ForEach(wsRef =>
                {
                    Console.WriteLine($"WSRefs: {wsRef.Id}: {wsRef.Url}, {wsRef.Description}, {wsRef.Minus}, {wsRef.Plus}");
                });

                // Test GetAllComment
                repo.GetAllComment().ForEach(comment =>
                {
                    Console.WriteLine($"Comments {comment.Id}: {comment.Commtext}, {comment.Stamp}, {comment.WSrefId}");
                });

                // Test AddWSRef
                if (repo.AddWSRef(new WSRef { Url = "https://www.belstu.by/", Description = "БГТУ", Minus = 0, Plus = 0 }))
                    Console.WriteLine("WSRefs: Add");
                else
                    Console.WriteLine("WSRefs: Error Add");

                // Test AddComment
                if (repo.AddComment(new Comment { WSrefId = 3, Commtext = "test", Stamp = DateTime.Now }))
                    Console.WriteLine("Comments: Add");
                // Test плохой WSRef
                if (repo.AddComment(new Comment { WSrefId = 123456, Commtext = "test", Stamp = DateTime.Now }))
                    Console.WriteLine("Comments: Add");
                else
                    Console.WriteLine("Comments: Error Add");

                Console.WriteLine("After addWSRef, addComment");

                // Display updated data
                repo.GetAllWSRef().ForEach(wsRef =>
                {
                    Console.WriteLine($"WSRefs: {wsRef.Id}: {wsRef.Url}, {wsRef.Description}, {wsRef.Minus}, {wsRef.Plus}");
                });

                repo.GetAllComment().ForEach(comment =>
                {
                    Console.WriteLine($"Comments {comment.Id}: {comment.Commtext}, {comment.Stamp}, {comment.WSrefId}");
                });
            }

            Console.WriteLine("Finish");
            Console.ReadLine();
        }

        private static void InitializeData(string fileName)
        {
            using var context = Context.Create(fileName);
            if (!context.WSRefs.Any())
            {
                var refs = new List<WSRef>
                {
                    new WSRef { Description = "Oracle", Url = "https://www.oracle.com", Minus = 0, Plus = 0 },
                    new WSRef { Description = "Java", Url = "https://jakarta.ee/", Minus = 0, Plus = 0 },
                    new WSRef { Description = "JavaScript", Url = "https://ecma-international.org/", Minus = 0, Plus = 0 }
                };
                refs.ForEach(r => context.AddWSRef(r));
                context.SaveChanges();

                var comments = new List<Comment>();
                foreach (var wsRef in refs)
                {
                    comments.Add(new Comment { WSrefId = wsRef.Id, Stamp = DateTime.Now, Commtext = $"{wsRef.Id}-Comment1" });
                    comments.Add(new Comment { WSrefId = wsRef.Id, Stamp = DateTime.Now, Commtext = $"{wsRef.Id}-Comment2" });
                }
                comments.ForEach(c => context.AddComment(c));
                context.SaveChanges();
            }
        }
    }
}
