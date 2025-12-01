
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

    public class Celebrity // Знаменитость
    {
        public Celebrity()
        {
            this.FullName = "";
            this.Nationality = "XX";
        }

        public int Id             { get; set; }    // Id Знаменитости
        public string FullName    { get; set; }    // полное имя Знаменитости
        public string Nationality { get; set; }    // гражданство Знаменитости
    }

    public class Lifeevent // Событие в жизни знаменитости
    {
        public Lifeevent()
        {
            this.Description = "";
        }

        public int Id             { get; set; }  // Id События
        public int CelebrityId    { get; set; }  // Id Знаменитости
        public DateTime Date      { get; set; }  // дата События
        public string Description { get; set; }  // описание События
    }

    public interface IRepository : ICommon, ICelebrity, ILifeevent { }

    public interface ICelebrity : ICelebrity<Celebrity> { }
    public interface ILifeevent : ILifeevent<Lifeevent> { }
    public interface ICommon : ICommon<Celebrity, Lifeevent> { }

    public interface ICommon<T1, T2>
    {
        List<T2> GetLifeeventsByCelebrityId(int celebrityId); // получить все События по Id Знаменитости
        T1? GetCelebrityByLifeeventId(int lifeeventId);       // получить Знаменитость по Id События
    }

    public interface ICashService
    {
        void ResetCache();
    }

    public interface ICashCelebrities : ICashService
    {
        List<Celebrity> GetCachedCelebrities();
        DateTime LastUpdateTime { get; }
        TimeSpan CacheDuration  { get; set; }
    }
}
