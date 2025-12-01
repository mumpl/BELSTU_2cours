using REPO;
using DALJSONX;
using DALMSQLX;

namespace TEST_XX
{
    internal class Program
    {
        private static void Main(string[] args)
        {

            Console.WriteLine("Инициализация данных для DALMSQLX");
            InitializeDataMSQLX();


            Console.WriteLine("Инициализация данных для DALJSONX");
            InitializeDataJSONX("WSRef.json");


            Console.WriteLine("\nТестирование DALMSQLX:");
            TestRepository(DALMSQLX.Repository.Create());


            Console.WriteLine("\nТестирование DALJSONX:");
            TestRepository(DALJSONX.Repository.Create("WSRef.json"));

            Console.WriteLine("\nТестирование завершено. Нажмите Enter для выхода...");
            Console.ReadLine();
        }

        private static void TestRepository(IRepository repo)
        {
            using (repo)
            {
  
                Console.WriteLine("\nВсе WSRef:");
                repo.GetAllWSRef().ForEach(wsRef =>
                {
                    Console.WriteLine($"{wsRef.Id}: {wsRef.Url}, {wsRef.Description}, {wsRef.Minus}, {wsRef.Plus}");
                });

 
                Console.WriteLine("\nВсе Comment:");
                repo.GetAllComment().ForEach(comment =>
                {
                    Console.WriteLine($"{comment.Id}: {comment.Commtext}, {comment.Stamp}, {comment.WSrefId}");
                });


                Console.WriteLine("\nДобавление нового WSRef:");
                bool wsRefAdded = repo.AddWSRef(new WSRef
                {
                    Url = "https://www.example.com",
                    Description = "Пример",
                    Minus = 0,
                    Plus = 0
                });
                Console.WriteLine($"WSRef добавлен: {wsRefAdded}");


                Console.WriteLine("\nДобавление нового Comment:");
                bool commentAdded = repo.AddComment(new Comment
                {
                    WSrefId = 1,
                    Commtext = "Тестовый комментарий",
                    Stamp = DateTime.Now
                });
                Console.WriteLine($"Comment добавлен: {commentAdded}");

                Console.WriteLine("\nДобавление нового Comment ТЕСТ С НЕСУЩЕСТВУЮЩИМ WSRef :");
                commentAdded = repo.AddComment(new Comment
                {
                    WSrefId = 123456,
                    Commtext = "Тестовый комментарий",
                    Stamp = DateTime.Now
                });
                Console.WriteLine($"Comment добавлен: {commentAdded}");

                Console.WriteLine("\nПолучение Comment по Id (1):");
                Comment? comment = repo.GetCommentById(1);
                if (comment != null)
                {
                    Console.WriteLine($"Найден: {comment.Id}, {comment.Commtext}");
                }
                else
                {
                    Console.WriteLine("Комментарий не найден");
                }
            }
        }

        private static void InitializeDataMSQLX()
        {
            using var context = new DALMSQLX.Context();

            if (!context.WSRefs.Any())
            {

                var refs = new List<WSRef>
                {
                    new WSRef { Description = "Oracle", Url = "https://www.oracle.com", Minus = 03, Plus = 0 },
                    new WSRef { Description = "Java", Url = "https://jakarta.ee/", Minus = 02, Plus = 0 },
                    new WSRef { Description = "JavaScript", Url = "https://ecma-international.org/", Minus = 1, Plus = 0 }
                };
                context.WSRefs.AddRange(refs); 
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

        private static void InitializeDataJSONX(string fileName)
        {
            using var context = DALJSONX.Context.Create(fileName);

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
