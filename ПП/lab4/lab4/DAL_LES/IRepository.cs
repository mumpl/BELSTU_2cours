namespace DAL_LES
{
    public interface ICelebrity<T> : IDisposable
    {
        List<T> GetAllCelebrities();            // получить все Знаменитости
        T? GetCelebrityById(int Id);            // получить Знаменитость по Id
        bool DelCelebrity(int id);              // удалить Знаменитость по Id
        bool AddCelebrity(T celebrity);         // добавить Знаменитость
        bool UpdCelebrity(int id, T celebrity); // изменить Знаменитость по Id
    }

    public interface ILifeevent<T> : IDisposable
    {
        List<T> GetAllLifeevents();             // получить все События
        T? GetLifeeventById(int Id);            // получить Событие по Id
        bool DelLifeevent(int id);              // удалить Событие по Id
        bool AddLifeevent(T lifeevent);         // добавить Событие
        bool UpdLifeevent(int id, T lifeevent); // изменить событие по Id
    }
    public class Celebrity
    {
        public int Id { get; set; }
        public string? FullName { get; set; } = string.Empty;
        public string? Nationality { get; set; } = "XX";
        public string? ReqPhotoPath { get; set; } = string.Empty;

        public List<Lifeevent> Lifeevents { get; set; } = new();
    }

    public class Lifeevent
    {
        public int Id { get; set; }
        public int CelebrityId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? ReqPhotoPath { get; set; } = string.Empty;

        public Celebrity Celebrity { get; set; }
    }

    public interface ICelebrity : ICelebrity<Celebrity> { }
    public interface ILifeevent : ILifeevent<Lifeevent> { }
    public interface ICommon : ICommon<Celebrity, Lifeevent> { }

    public interface ICommon<T1, T2> : ICelebrity<T1>, ILifeevent<T2>
    {
        List<T2> GetLifeeventsByCelebrityId(int celebrityId);
        T1 GetCelebrityByLifeeventId(int lifeeventId);
    }
}
