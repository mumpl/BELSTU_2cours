namespace DAL_LES.Interfaces
{
    public interface ICommon<T1, T2> : IDisposable
    {
        List<T2> GetLifeEventsByCelebrityId(int celebrityId); // получить все События по Id Знаменитости
        T1 GetCelebrityByLifeEventId(int lifeEventId); // получить Знаменитость по Id События
    }
}
