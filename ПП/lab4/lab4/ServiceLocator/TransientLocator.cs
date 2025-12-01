namespace ServiceLocator
{
    public static class TransientLocator
    {
        private static readonly Dictionary<Type, Func<object>> services = new();   //хранит фабрики д созд сервисов

        public static void Register<T>(Func<T> resolver) where T : class           //Принимает resolver, который является фабрикой для созд объекта - переданная лямбда
        {
            services[typeof(T)] = () => resolver();                                //resolver(); вызывает функцию, которая создает объект
                                                                                   //services[typeof(T)] — ищет ключ Type и сохраняет фабрику для его создания
        }

        public static T Resolve<T>() where T : class                               ///ищет соответствующую фабрику, хранящую способ создания объекта типа T
        {
            return (T)services[typeof(T)]();                                       // 1. services[typeof(T)] - получаем фабричную функцию для типа T
                                                                                   // 2. () - вызываем эту функцию
                                                                                   // 3. (T) - приводим результат к типу T
        }

        public static void Reset()
        {
            services.Clear();
        }
    }
}
