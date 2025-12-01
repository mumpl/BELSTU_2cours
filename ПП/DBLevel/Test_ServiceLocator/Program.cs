using ServiceLocator;

public interface IMessageService
{
    void SendMessage(string message);
}

public class ConsoleMessageService : IMessageService, IDisposable
{
    private static int _counter = 0;
    private readonly int _id;

    public ConsoleMessageService()
    {
        _id = ++_counter;
        Console.WriteLine($"Сервис создан #{_id}");
    }

    public void SendMessage(string message)
    {
        Console.WriteLine($"[{_id}] {message}");
    }

    public void Dispose()
    {
        Console.WriteLine($"Объект уничтожен: #{_id}");
    }
}

class Program
{
    static void Main()
    {
        var locator = new ServiceLocator.ServiceLocator();

        locator.Register<IMessageService>(() => new ConsoleMessageService(), ServiceLifetime.Transient);         //каждый раз новый
        locator.Register<ConsoleMessageService>(() => new ConsoleMessageService(), ServiceLifetime.Singleton);   //создан 1 раз

        Console.WriteLine("\n*** Transient сервисы ***");
        var transient1 = locator.GetService<IMessageService>();
        var transient2 = locator.GetService<IMessageService>();
        transient1.SendMessage("Transient 1");
        transient2.SendMessage("Transient 2");
        Console.WriteLine($"Один экземпляр? {transient1 == transient2}");

        Console.WriteLine("\n*** Singleton сервисы ***");
        var singleton1 = locator.GetService<ConsoleMessageService>();
        var singleton2 = locator.GetService<ConsoleMessageService>();
        singleton1.SendMessage("Singleton 1");
        singleton2.SendMessage("Singleton 2");
        Console.WriteLine($"Один экземпляр? {singleton1 == singleton2}");

        Console.WriteLine("\n*** Scoped сервисы ***");
        // Переопределяем как Scoped
        locator.Register<IMessageService>(() => new ConsoleMessageService(), ServiceLifetime.Scoped);

        using (var scope1 = locator.CreateScope())
        {
            Console.WriteLine("--- Scope 1 ---");
            var scoped1a = scope1.ServiceLocator.GetService<IMessageService>();
            var scoped1b = scope1.ServiceLocator.GetService<IMessageService>();
            scoped1a.SendMessage("Scoped 1-A");
            scoped1b.SendMessage("Scoped 1-B");
            Console.WriteLine($"Один экземпляр в scope? {scoped1a == scoped1b}");
        }

        using (var scope2 = locator.CreateScope())
        {
            Console.WriteLine("--- Scope 2 ---");
            var scoped2 = scope2.ServiceLocator.GetService<IMessageService>();
            scoped2.SendMessage("Scoped 2");
        }

        Console.WriteLine("\n*** Очистка ***");
        locator.Dispose();
    }
}