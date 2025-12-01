using Microsoft.Extensions.DependencyInjection;

public class ServiceScope : IServiceScope
{
    private readonly Dictionary<Type, object> _scoped = new();
    public IServiceProvider ServiceProvider { get; }

    public ServiceScope(Dictionary<Type, Func<IServiceProvider, object>> services)
    {
        ServiceProvider = new ServiceProvider(services, _scoped);
    }

    public void Dispose()
    {
        _scoped.Clear();
    }
}
