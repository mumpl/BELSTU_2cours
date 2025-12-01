using DAL21;
using System.Security.Cryptography.X509Certificates;

internal class Program
{
    private static void Main(string[] args)
    {

        Console.WriteLine("------>Start \n");
        Init.Execute();
        Console.WriteLine("\n------>AfterExecute");


        using (IRepository repo = Repository.Create())
        {
            repo.getAllComment().ForEach(comment => {
                Console.WriteLine($"{comment.Id}: {comment.Commtext}, {comment.Stamp}, {comment.WSrefId} ");
            });

            repo.getAllWSRef().ForEach(wsRef => {
                Console.WriteLine($"{wsRef.Id}: {wsRef.Url}, {wsRef.Description}, {wsRef.Minus}, {wsRef.Plus}");
            });
            repo.addWSRef(new WSRef() { Url = "https://www.belstu.by/", Description = "БГТУ", Minus = 0, Plus = 1 });
            {
                Comment? c = repo.GetCommentById(1);
                 if (c != null) Console.WriteLine($" repo.GetCommentById(1) = {c.Id}, {c.Commtext} ");
            }
            {
                Comment? c = repo.GetCommentById(1111);
                if (c != null) Console.WriteLine($" repo.GetCommentById(1) = {c.Id}, {c.Commtext} ");
            }
            if (repo.addComment(new Comment()
                                {
                                    WSrefId = 3,
                                    Commtext = "test",
                                    Stamp = DateTime.Now
                                }))
            {
                Console.WriteLine("Comments: Add");
            }
            else Console.WriteLine("коомментарий не добавлен.");
            //Тестирование с неправильным WSRef
            if (repo.addComment(new Comment()
                                {
                                    WSrefId = 123456,
                                    Commtext = "test",
                                    Stamp = DateTime.Now
                                }))
            {
                Console.WriteLine("Comments: Add");
            }
            else Console.WriteLine("коомментарий не добавлен.");





            Console.WriteLine("------------------------------------------------------------------------");
            repo.getAllWSRef().ForEach(wsRef => {
                Console.WriteLine($"{wsRef.Id}: {wsRef.Url}, {wsRef.Description}, {wsRef.Minus}, {wsRef.Plus}");
            });
        }





        Console.ReadLine();
    }
}