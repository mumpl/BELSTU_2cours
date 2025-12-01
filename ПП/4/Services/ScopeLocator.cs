using Microsoft.Extensions.DependencyInjection;
namespace CashCelebrities { 
public static class ScopeLocator
{
        private static readonly Dictionary<Type, Func<TimeSpan, object>> Services = new Dictionary<Type, Func<TimeSpan, object>>();

        public static void Register<T>(Func<TimeSpan, T> resolver) where T : class
        {
            Services[typeof(T)] = cacheDuration => resolver(cacheDuration);
        }

        public static IServiceScopeFactory CreateServiceScopeFactory(TimeSpan cacheDuration)
        {
            return new ServiceScopeFactory(Services, cacheDuration);
        }
    }
}