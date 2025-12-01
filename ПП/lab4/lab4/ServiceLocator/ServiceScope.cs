using Microsoft.Extensions.DependencyInjection;

namespace ServiceLocator
{
    public class ServiceScope : IServiceScope
    { 
        private readonly Dictionary<Type, object> _scoped = new();   //объекты сервисов, в рамках текущей области
        public IServiceProvider ServiceProvider { get; }

        public ServiceScope(Dictionary<Type, Func<IServiceProvider, object>> services)       //словарь фабрик и ссылкой на _scoped
        {
            ServiceProvider = new ServiceProvider(services, _scoped);
        }

        public void Dispose()
        {
            _scoped.Clear();
        }
    }
}
