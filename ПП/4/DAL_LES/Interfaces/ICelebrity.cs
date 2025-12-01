namespace DAL_LES.Interfaces
{
    public interface ICelebrity<T> : IDisposable
    {
        List<T> GetAllCelebrities(); // получить все Знаменитости
        T? GetCelebrityById(int Id); // получить Знаменитость по Id
        bool DelCelebrity(int id); // удалить Знаменитость по Id
        bool AddCelebrity(T celebrity); // добавить Знаменитость
        bool UpdCelebrity(int id, T celebrity); // изменить Знаменитость по Id
    }
}
