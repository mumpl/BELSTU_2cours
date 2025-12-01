// Program.cs
using DALMSQLX;
using REPO;
using System;

namespace Test_DALMSSQLX
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("------>Start\n");
            InitializeData();
            Console.WriteLine("\n------>After Initialization");

            using (IRepository repo = Repository.Create())
            {
                // Тест получения всех комментариев
                Console.WriteLine("\nAll Comments:");
                repo.GetAllComment().ForEach(comment =>
                {
                    Console.WriteLine($"{comment.Id}: {comment.Commtext}, {comment.Stamp}, {comment.WSrefId}");
                });

                // Тест получения всех ссылок
                Console.WriteLine("\nAll WSRefs:");
                repo.GetAllWSRef().ForEach(wsRef =>
                {
                    Console.WriteLine($"{wsRef.Id}: {wsRef.Url}, {wsRef.Description}, {wsRef.Minus}, {wsRef.Plus}");
                });

                // Тест добавления новой ссылки
                Console.WriteLine("\nAdding new WSRef:");
                bool wsRefAdded = repo.AddWSRef(new WSRef()
                {
                    Url = "https://www.belstu.by/",
                    Description = "БГТУ",
                    Minus = 0,
                    Plus = 1
                });
                Console.WriteLine($"WSRef added: {wsRefAdded}");

                // Тест получения комментария по существующему ID
                Console.WriteLine("\nTesting GetCommentById with existing ID (1):");
                Comment? existingComment = repo.GetCommentById(1);
                if (existingComment != null)
                    Console.WriteLine($"Found: {existingComment.Id}, {existingComment.Commtext}");

                // Тест получения комментария по несуществующему ID
                Console.WriteLine("\nTesting GetCommentById with non-existing ID (1111):");
                Comment? nonExistingComment = repo.GetCommentById(1111);
                if (nonExistingComment == null)
                    Console.WriteLine("Comment not found (expected)");

                // Тест добавления нового комментария
                Console.WriteLine("\nAdding new Comment:");
                bool commentAdded = repo.AddComment(new Comment()
                {
                    WSrefId = 3,
                    Commtext = "test",
                    Stamp = DateTime.Now
                });
                Console.WriteLine(commentAdded ? "Comment added successfully" : "Failed to add comment");
                // Тест добавления нового комментария c несуществующим айди
                Console.WriteLine("\nAdding new Comment:");
                 commentAdded = repo.AddComment(new Comment()
                {
                    WSrefId = 123456,
                    Commtext = "test",
                    Stamp = DateTime.Now
                });
                Console.WriteLine(commentAdded ? "Comment added successfully" : "Failed to add comment");

                // Показать обновленный список ссылок
                Console.WriteLine("\nUpdated WSRefs list:");
                repo.GetAllWSRef().ForEach(wsRef =>
                {
                    Console.WriteLine($"{wsRef.Id}: {wsRef.Url}, {wsRef.Description}, {wsRef.Minus}, {wsRef.Plus}");
                });
            }

            Console.WriteLine("\nPress Enter to exit...");
            Console.ReadLine();
        }

        private static void InitializeData()
        {
            using var context = new Context();
            if (!context.WSRefs.Any())
            {
                var refs = new List<WSRef>()
                {
                    new WSRef() { Description = "Oracle", Url = "https://www.oracle.com", Minus = 0, Plus = 0 },
                    new WSRef() { Description = "Java", Url = "https://jakarta.ee/", Minus = 0, Plus = 0 },
                    new WSRef() { Description = "JavaScript", Url = "https://ecma-international.org/", Minus = 0, Plus = 0 }
                };
                context.WSRefs.AddRange(refs);
                context.SaveChanges();

                var comments = new List<Comment>();
                foreach (var wsRef in refs)
                {
                    comments.Add(new Comment() { WSrefId = wsRef.Id, Stamp = DateTime.Now, Commtext = $"{wsRef.Id}-Comment1" });
                    comments.Add(new Comment() { WSrefId = wsRef.Id, Stamp = DateTime.Now, Commtext = $"{wsRef.Id}-Comment2" });
                }
                context.Comments.AddRange(comments);
                context.SaveChanges();
            }
        }
    }
}