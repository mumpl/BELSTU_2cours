using System;
using Microsoft.Extensions.DependencyInjection;
using ServiceLocator;

public interface IRepository : IDisposable
{
    Guid Id { get; }
}

public class Repository : IRepository
{
    public Guid Id { get; } = Guid.NewGuid();

    public static IRepository Create()
    {
        return new Repository();
    }

    public void Dispose()
    {
     
    }
}

class Program
{
    static void Main()
    {
        TransientLocator.Register<IRepository>(() => Repository.Create());

        using (IRepository repo = TransientLocator.Resolve<IRepository>())
        {
            Console.WriteLine($"Transient: {repo.Id}");
        }

        using (IRepository repo = TransientLocator.Resolve<IRepository>())
        {
            Console.WriteLine($"Transient: {repo.Id}");
        }

        Console.WriteLine();

        SingletonLocator.Register<IRepository>(Repository.Create());

        using (IRepository repo = SingletonLocator.Resolve<IRepository>())
        {
            Console.WriteLine($"Singleton: {repo.Id}");
        }

        using (IRepository repo = SingletonLocator.Resolve<IRepository>())
        {
            Console.WriteLine($"Singleton: {repo.Id}");
        }

        Console.WriteLine();

        //ScopeLocator.Register<IRepository>(() => Repository.Create());
        ScopeLocator.Register<IRepository>(sp => Repository.Create());

        IServiceScopeFactory scopeFactory = ScopeLocator.CreateServiceScopeFactory();

        using (IServiceScope scope1 = scopeFactory.CreateScope())
        {
            IRepository repo1 = scope1.ServiceProvider.GetService<IRepository>();
            IRepository repo2 = scope1.ServiceProvider.GetService<IRepository>();
            Console.WriteLine($"Scope 1 - Repo 1: {repo1.Id}");
            Console.WriteLine($"Scope 1 - Repo 2: {repo2.Id}");
        }

        using (IServiceScope scope2 = scopeFactory.CreateScope())
        {
            IRepository repo3 = scope2.ServiceProvider.GetService<IRepository>();
            Console.WriteLine($"Scope 2 - Repo: {repo3.Id}");
        }
    }
}
