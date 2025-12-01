public class ServiceProvider : IServiceProvider
{
    private readonly Dictionary<Type, Func<TimeSpan, object>> _services;
    private readonly Dictionary<Type, object> _scopedServices;
    private readonly TimeSpan _cacheDuration;

    public ServiceProvider(Dictionary<Type, Func<TimeSpan, object>> services, Dictionary<Type, object> scopedServices, TimeSpan cacheDuration)
    {
        _services = services;
        _scopedServices = scopedServices;
        _cacheDuration = cacheDuration;
    }

    public object GetService(Type serviceType)
    {
        if (_scopedServices.TryGetValue(serviceType, out var service))
            return service;

        if (_services.TryGetValue(serviceType, out var factory))
        {
            service = factory(_cacheDuration);
            _scopedServices[serviceType] = service;
            return service;
        }

        return null;
    }
}