
namespace ServiceLocator
{
    public static class TransientLocator
    {
        private readonly static Dictionary<Type, Func<object>> services = new Dictionary<Type, Func<object>>();

        public static void Register<T>(Func<T> resolver) where T : class
        {
            TransientLocator.services[typeof(T)] = () => resolver();
        }

        public static T Resolve<T>() where T : class
        {
            return (T)TransientLocator.services[typeof(T)]();
        }

        public static void Reset()
        {
            TransientLocator.services.Clear();
        }
    }
}
