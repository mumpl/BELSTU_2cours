using Microsoft.Extensions.DependencyInjection;
using ServiceLocator;

public static class ScopeLocator
{
    private static readonly Dictionary<Type, Func<IServiceProvider, object>> services = new();

    public static void Register<T>(Func<IServiceProvider, T> factory) where T : class
    {
        services[typeof(T)] = sp => factory(sp);
    }

    public static IServiceScopeFactory CreateServiceScopeFactory()
    {
        return new ServiceScopeFactory(services);
    }
}
