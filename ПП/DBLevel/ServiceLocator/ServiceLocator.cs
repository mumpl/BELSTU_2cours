
namespace ServiceLocator
{
    public class ServiceLocator : IServiceLocator, IDisposable
    {                                                //д созд экз
        protected internal readonly Dictionary<Type, (Func<object> factory, ServiceLifetime lifecycle)> _registrations = new();
        private readonly Dictionary<Type, object> _singletonInstances = new();       //созд экз сервисов Singleton
        private bool _disposed;      

        public void Register<T>(Func<T> factory, ServiceLifetime lifecycle = ServiceLifetime.Transient)
        {
            _registrations[typeof(T)] = (() => factory(), lifecycle); //упаковка фактори и жц
        }

        public T GetService<T>()
        {
            if (_disposed) throw new ObjectDisposedException(nameof(ServiceLocator));

            var type = typeof(T);
            if (!_registrations.TryGetValue(type, out var registration))
                throw new InvalidOperationException($"Сервис {type.Name} не зарегистрирован");

            return registration.lifecycle switch
            {
                ServiceLifetime.Singleton => GetSingleton<T>(type, registration.factory),
                ServiceLifetime.Transient => (T)registration.factory(),                    //каждый раз новый экз
                _ => throw new InvalidOperationException("Scoped не реализован в текущем коде")
            };
        }

        protected internal T GetSingleton<T>(Type type, Func<object> factory)
        {
            if (!_singletonInstances.TryGetValue(type, out var instance))  //если инстанс не создан
                _singletonInstances[type] = instance = factory();          
            return (T)instance;                                            
        } 

        public IServiceScope CreateScope() => new ServiceScope(this);

        public void Dispose()
        {
            if (_disposed) return;

            foreach (var instance in _singletonInstances.Values)
                (instance as IDisposable)?.Dispose();           //если объект реализует IDisposable вызываем метод

            _singletonInstances.Clear();                        
            _disposed = true;
        }
    }
}
