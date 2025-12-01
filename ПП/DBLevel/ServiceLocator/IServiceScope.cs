

namespace ServiceLocator
{
    public interface IServiceScope : IDisposable
    {
        IServiceLocator ServiceLocator { get; }
    }
}
