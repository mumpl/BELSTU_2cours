using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using DALMSQLXG;


namespace Test_DALMSQLXG
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Указываем строку подключения напрямую
            string connectionString = "Server=USER-PC;Database=Patterns;Trusted_Connection=True;Encrypt=False;";

            // Настройка DbContext с реальной базой данных
            var options = new DbContextOptionsBuilder<DALMSQLXGContext>()
                .UseSqlServer(connectionString)
                .Options;

            Console.WriteLine("------>Start\n");

            InitializeData(options);

            Console.WriteLine("\n------>After Initialization");

            using (var context = new DALMSQLXGContext(options))
            using (var repo = new Repository(context))
            {
                // Тест получения всех комментариев
                Console.WriteLine("\nAll Comments:");
                repo.getAllComment().ForEach(comment =>
                {
                    Console.WriteLine($"{comment.Id}: {comment.Commtext}, {comment.Stamp}, {comment.WSrefId}");
                });

                // Тест получения всех ссылок
                Console.WriteLine("\nAll WSRefs:");
                repo.getAllWSRef().ForEach(wsRef =>
                {
                    Console.WriteLine($"{wsRef.Id}: {wsRef.Url}, {wsRef.Description}, {wsRef.Minus}, {wsRef.Plus}");
                });

                // Тест добавления новой ссылки
                Console.WriteLine("\nAdding new WSRef:");
                bool wsRefAdded = repo.addWSRef(new WSref
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
                bool commentAdded = repo.addComment(new Comment
                {
                    WSrefId = 3,
                    Commtext = "test",
                    Stamp = DateTime.Now
                });
                Console.WriteLine(commentAdded ? "Comment added successfully" : "Failed to add comment");
                // Тест добавления нового комментария несуществующим WSREF
                Console.WriteLine("\nAdding new Comment:");
                commentAdded = repo.addComment(new Comment
                {
                    WSrefId = 123456,
                    Commtext = "test",
                    Stamp = DateTime.Now
                });
                Console.WriteLine(commentAdded ? "Comment added successfully" : "Failed to add comment");

                // Показать обновленный список ссылок
                Console.WriteLine("\nUpdated WSRefs list:");
                repo.getAllWSRef().ForEach(wsRef =>
                {
                    Console.WriteLine($"{wsRef.Id}: {wsRef.Url}, {wsRef.Description}, {wsRef.Minus}, {wsRef.Plus}");
                });
            }

            Console.WriteLine("\nPress Enter to exit...");
            Console.ReadLine();
        }

        private static void InitializeData(DbContextOptions<DALMSQLXGContext> options)
        {
            using var context = new DALMSQLXGContext(options);
            if (!context.WSrefs.Any())
            {
                var refs = new List<WSref>
                {
                    new WSref { Description = "Oracle", Url = "https://www.oracle.com", Minus = 0, Plus = 0 },
                    new WSref { Description = "Java", Url = "https://jakarta.ee/", Minus = 0, Plus = 0 },
                    new WSref { Description = "JavaScript", Url = "https://ecma-international.org/", Minus = 0, Plus = 0 }
                };
                context.WSrefs.AddRange(refs);
                context.SaveChanges();

                var comments = new List<Comment>();
                foreach (var wsRef in refs)
                {
                    comments.Add(new Comment { WSrefId = wsRef.Id, Stamp = DateTime.Now, Commtext = $"{wsRef.Id}-Comment1" });
                    comments.Add(new Comment { WSrefId = wsRef.Id, Stamp = DateTime.Now, Commtext = $"{wsRef.Id}-Comment2" });
                }
                context.Comments.AddRange(comments);
                context.SaveChanges();
            }
        }
    }
}