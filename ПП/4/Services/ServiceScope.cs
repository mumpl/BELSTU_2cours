using Microsoft.Extensions.DependencyInjection;

public class ServiceScope : IServiceScope
{
    private readonly Dictionary<Type, Func<TimeSpan, object>> _services;
    private readonly TimeSpan _cacheDuration;
    private readonly Dictionary<Type, object> _scopedServices = new Dictionary<Type, object>();

    public IServiceProvider ServiceProvider { get; }

    public ServiceScope(Dictionary<Type, Func<TimeSpan, object>> services, TimeSpan cacheDuration)
    {
        _services = services;
        _cacheDuration = cacheDuration;
        ServiceProvider = new ServiceProvider(_services, _scopedServices, _cacheDuration);
    }

    public void Dispose()
    {
        _scopedServices.Clear();
    }
}