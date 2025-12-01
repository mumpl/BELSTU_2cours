using Microsoft.Extensions.DependencyInjection;

namespace ServiceLocator
{
    public static class ServiceLocator 
    {
        private readonly static Dictionary<Type, Func<object>> services = new Dictionary<Type, Func<object>>();

        public static void Register<T>(Func<T> resolver) where T : class
        {
            ScopeLocator.services[typeof(T)] = () => resolver();
        }

        public static IServiceScopeFactory CreateServiceScopeFactory()
        {
            return new ServiceScopeFactory(services);
        }
    }
}
