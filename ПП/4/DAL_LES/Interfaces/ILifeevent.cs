namespace DAL_LES.Interfaces
{
    public interface ILifeEvent<T> : IDisposable
    {
        List<T> GetAllLifeEvents(); // получить все События
        T? GetLifeEventById(int Id); // получить Событие по Id
        bool DelLifeEvent(int id); // удалить Событие по Id
        bool AddLifeEvent(T lifeEvent); // добавить Событие
        bool UpdLifeEvent(int id, T lifeEvent); // изменить Событие по Id
    }
}
