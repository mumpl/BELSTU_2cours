using Microsoft.Extensions.DependencyInjection;
namespace ServiceLocator
{
    public class ServiceScopeFactory : IServiceScopeFactory                                     //созд скоупов
    {
        public IServiceScope CreateScope()                                                     //созд новая область, кот предается словарь фабрик
        {
            return new ServiceScope(_services);
        }
         
        private readonly Dictionary<Type, Func<IServiceProvider, object>> _services;           //словарь фабрик, кот созд сервисы

        public ServiceScopeFactory(Dictionary<Type, Func<IServiceProvider, object>> services)
        {
            _services = services;
        }
    }
}
