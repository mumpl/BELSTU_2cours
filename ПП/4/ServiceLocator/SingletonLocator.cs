namespace ServiceLocator
{
    // SingletonLocator.Register<IRepository>(JSON_DAL.Repository.Create());
    // SingletonLocator.Register<IRepository>(MSSQL_DAL.Repository.Create());

    public static class SingletonLocator
    {
        private readonly static Dictionary<Type, object> services = new Dictionary<Type, object>();

        public static void Register<T>(T service) where T : class
        {
            SingletonLocator.services[typeof(T)] = service;
        }

        public static T Resolve<T>()
        {
            return (T)SingletonLocator.services[typeof(T)];
        }

        public static void Reset()
        {
            SingletonLocator.services.Clear();
        }
    }
}
