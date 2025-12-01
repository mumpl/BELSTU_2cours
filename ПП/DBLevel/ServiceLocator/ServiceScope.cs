using DAL_LES;

namespace ServiceLocator
{
    public class ServiceScope : IServiceScope
    {
        private readonly ServiceLocator _parent;
        private readonly Dictionary<Type, object> _scopedInstances = new();
        private bool _disposed;

        public IServiceLocator ServiceLocator { get; }

        public ServiceScope(ServiceLocator parent)
        {
            _parent = parent;
            ServiceLocator = new ScopedServiceLocator(this);      //новый локатор, завяз на этом скоуп
        }

        public void Dispose()
        {
            if (_disposed) return;

            foreach (var instance in _scopedInstances.Values)
                (instance as IDisposable)?.Dispose();

            _scopedInstances.Clear();
            _disposed = true;
        }

        private class ScopedServiceLocator : IServiceLocator
        {
            private readonly ServiceScope _scope;

            public ScopedServiceLocator(ServiceScope scope) => _scope = scope;  //ссылка на тек скоуп

            public T GetService<T>()
            {
                if (_scope._disposed) throw new ObjectDisposedException(nameof(ServiceScope));

                var type = typeof(T);                                                             //зарег ли в родит локаторе
                if (!_scope._parent._registrations.TryGetValue(type, out var registration))
                    throw new InvalidOperationException($"Сервис {type.Name} не зарегистирован");

                return registration.lifecycle switch
                {
                    ServiceLifetime.Singleton => _scope._parent.GetSingleton<T>(type, registration.factory),   //делегируем родительскому локатору
                    ServiceLifetime.Scoped => GetScoped<T>(type, registration.factory),                        //для кеширования в пределах текущего scope
                    ServiceLifetime.Transient => (T)registration.factory(),
                    _ => throw new ArgumentOutOfRangeException()
                };
            }

            private T GetScoped<T>(Type type, Func<object> factory)
            {
                if (!_scope._scopedInstances.TryGetValue(type, out var instance))
                    _scope._scopedInstances[type] = instance = factory();
                return (T)instance;
            }

            public IServiceScope CreateScope() => _scope._parent.CreateScope();              //каждый scope может создать свой дочерний scope
        }
    }
}
