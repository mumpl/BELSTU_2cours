using Microsoft.Extensions.DependencyInjection;
using ServiceLocator;

public class ServiceScopeFactory : IServiceScopeFactory
{
    private readonly Dictionary<Type, Func<TimeSpan, object>> _services;
    private readonly TimeSpan _cacheDuration;

    public ServiceScopeFactory(Dictionary<Type, Func<TimeSpan, object>> services, TimeSpan cacheDuration)
    {
        _services = services;
        _cacheDuration = cacheDuration;
    }

    public IServiceScope CreateScope()
    {
        return new ServiceScope(_services, _cacheDuration);
    }
}