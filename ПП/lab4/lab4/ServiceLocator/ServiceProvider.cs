namespace ServiceLocator
{
    public class ServiceProvider : IServiceProvider
    {
        private readonly Dictionary<Type, Func<IServiceProvider, object>> _services; //хранит функции принимают IServiceProvider и возвращают объект сервиса
        private readonly Dictionary<Type, object> _scoped;                           //хранит созд экз серсисов

        public ServiceProvider(Dictionary<Type, Func<IServiceProvider, object>> services, Dictionary<Type, object> scoped)
        {
            _services = services;                                                    //словарь фабрик сервисов
            _scoped = scoped;                                                        //словарь для хранения созданных экземпляров сервисов в текущей области
        }

        public object GetService(Type serviceType)
        {                                         
            if (!_scoped.TryGetValue(serviceType, out var obj))                 
            {
                obj = _services[serviceType](this);                             //_services - словарь фабрик, [serviceType] - получает фабрику для конкретного типа (IRepository), this - текущий экземпляр ServiceProvider
                _scoped[serviceType] = obj;                                     //сохраняет созд сервис в словаре
            }
            return obj;
        }
    }
}