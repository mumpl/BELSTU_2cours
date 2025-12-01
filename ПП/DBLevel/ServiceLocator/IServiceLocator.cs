namespace ServiceLocator
{
    public interface IServiceLocator
    {
        T GetService<T>();  //получает экземпляры сервисов из контейнера
        IServiceScope CreateScope();
    }
}
