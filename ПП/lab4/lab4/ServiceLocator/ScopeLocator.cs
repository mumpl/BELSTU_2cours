using Microsoft.Extensions.DependencyInjection;

namespace ServiceLocator
{
    public static class ScopeLocator
    {
        public static IServiceScopeFactory CreateServiceScopeFactory()
        {
            return new ServiceScopeFactory(services);                    //передается ссылка на словарь сервисы
        }

        private static readonly Dictionary<Type, Func<IServiceProvider, object>> services = new();  //словарь зарегистрированных сервисов
                                                                                                    

        public static void Register<T>(Func<T> factory) where T : class    
        {
            services[typeof(T)] = sp => factory();                                         //сохраняет фабрику в словаре
        }  
    }
}
