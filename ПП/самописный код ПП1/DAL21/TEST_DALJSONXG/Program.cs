using System;
using System.Collections.Generic;
using DALJSONXG;

namespace Test_DALJSONXG
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            const string fileName = "WSRef.json";
            Console.WriteLine("Init");

            InitializeData(fileName);

            Console.WriteLine("Start");
            using (var repo = new DALJSONXG.Repository(fileName))
            {
                // Test getAllWSRef
                repo.getAllWSRef().ForEach(wsRef =>
                {
                    Console.WriteLine($"WSRefs: {wsRef.Id}: {wsRef.Url}, {wsRef.Description}, {wsRef.Minus}, {wsRef.Plus}");
                });

                // Test getAllComment
                repo.getAllComment().ForEach(comment =>
                {
                    Console.WriteLine($"Comments {comment.Id}: {comment.Commtext}, {comment.Stamp}, {comment.WSrefId}");
                });

                // Test addWSRef
                if (repo.addWSRef(new WSref { Url = "https://www.belstu.by/", Description = "БГТУ", Minus = 0, Plus = 0 }))
                    Console.WriteLine("WSRefs: Add");
                else
                    Console.WriteLine("WSRefs: Error Add");

                // Test addComment
                if (repo.addComment(new Comment { WSrefId = 3, Commtext = "test", Stamp = DateTime.Now }))
                    Console.WriteLine("Comments: Add");
                else
                    Console.WriteLine("Comments: Error Add");
                // Test addComment ПЛОХОЙ
                if (repo.addComment(new Comment { WSrefId = 123456, Commtext = "test", Stamp = DateTime.Now }))
                    Console.WriteLine("Comments: Add");
                else
                    Console.WriteLine("Comments: Error Add");

                Console.WriteLine("After addWSRef, addComment");

                // Display updated data
                repo.getAllWSRef().ForEach(wsRef =>
                {
                    Console.WriteLine($"WSRefs: {wsRef.Id}: {wsRef.Url}, {wsRef.Description}, {wsRef.Minus}, {wsRef.Plus}");
                });

                repo.getAllComment().ForEach(comment =>
                {
                    Console.WriteLine($"Comments {comment.Id}: {comment.Commtext}, {comment.Stamp}, {comment.WSrefId}");
                });
            }

            Console.WriteLine("Finish");
            Console.ReadLine();
        }

        private static void InitializeData(string fileName)
        {
            using var context = new DALJSONXG.Context(fileName);
            if (!context.WSRefs.Any())
            {
                var refs = new List<WSref>
                {
                    new WSref { Description = "Oracle", Url = "https://www.oracle.com", Minus = 0, Plus = 0 },
                    new WSref { Description = "Java", Url = "https://jakarta.ee/", Minus = 0, Plus = 0 },
                    new WSref { Description = "JavaScript", Url = "https://ecma-international.org/", Minus = 0, Plus = 0 }
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