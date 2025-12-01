using Microsoft.Extensions.DependencyInjection;
using ServiceLocator;

public class ServiceScopeFactory : IServiceScopeFactory
{
    private readonly Dictionary<Type, Func<IServiceProvider, object>> _services;

    public ServiceScopeFactory(Dictionary<Type, Func<IServiceProvider, object>> services)
    {
        _services = services;
    }

    public IServiceScope CreateScope()
    {
        return new ServiceScope(_services);
    }
}
