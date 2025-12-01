using DAL_LES.Interfaces;
using DAL_LES.Models;
namespace DAL_LES
{
    public interface ICommon : ICommon<Celebrity, LifeEvent> { }
    public interface ICelebrity : ICelebrity<Celebrity> { }
    public interface ILifeEvent : ILifeEvent<LifeEvent> { }
    public interface IRepository : ICommon, ICelebrity, ILifeEvent { 
        Guid Id { get; }
    }
    public class Repository : IRepository
    {
        private readonly SQLContext _context;
        public Guid Id { get; } = Guid.NewGuid();

        public static Repository Create()
        {
            return new Repository();
        }
        private Repository()
        {
            _context = new SQLContext();
        }
        public bool AddCelebrity(Celebrity celebrity)
        {
            try
            {
                _context.Add(celebrity);
                _context.SaveChanges();
            }
            catch (Exception e) {
                Console.WriteLine($"[AddCelebrity] - {e.Message}");
                return false;
            }
            return true;
        }

        public bool AddLifeEvent(LifeEvent lifeEvent)
        {
            try
            {
                _context.Add(lifeEvent);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine($"[AddLifeEvent] - {e.Message}");
                return false;
            }
            return true;
        }

        public bool DelCelebrity(int id)
        {
            try
            {
                Celebrity celebrity = _context.Celebrities.FirstOrDefault(c => c.Id == id) ?? new Celebrity();
                _context.Celebrities.Remove(celebrity);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine($"[DelCelebrity] - {e.Message}");
                return false;
            }
            return true;
        }

        public bool DelLifeEvent(int id)
        {
            try
            {
                LifeEvent lifeEvent = _context.LifeEvents.FirstOrDefault(elem => elem.Id == id) ?? new LifeEvent();
                _context.LifeEvents.Remove(lifeEvent);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine($"[DelLifeEvent] - {e.Message}");
                return false;
            }
            return true;
        }
        public List<Celebrity> GetAllCelebrities()
        {
            return _context.Celebrities.ToList();
        }

        public List<LifeEvent> GetAllLifeEvents()
        {
            return _context.LifeEvents.ToList();
        }

        public Celebrity? GetCelebrityById(int Id)
        {
            return _context.Celebrities.FirstOrDefault(elem => elem.Id == Id);
        }

        public Celebrity GetCelebrityByLifeEventId(int lifeEventId)
        {
            return _context.Celebrities.Where(elem => elem.Id == lifeEventId).ElementAt(0);
        }

        public LifeEvent? GetLifeEventById(int Id)
        {
            return _context.LifeEvents.FirstOrDefault(elem => elem.Id == Id);
        }

        public List<LifeEvent> GetLifeEventsByCelebrityId(int celebrityId)
        {
            return _context.LifeEvents.Where(elem => elem.CelebrityId == celebrityId).ToList();
        }

        public bool UpdCelebrity(int id, Celebrity celebrity)
        {
            Celebrity sel_celebrity = _context.Celebrities.FirstOrDefault(elem => elem.Id == id) ?? new Celebrity();
            _context.Celebrities.Update(sel_celebrity);
            return _context.SaveChanges() > 0 ? true : false;
        }

        public bool UpdLifeEvent(int id, LifeEvent lifeEvent)
        {
            LifeEvent sellifeEvent = _context.LifeEvents.FirstOrDefault(elem => elem.Id == id) ?? new LifeEvent();
            _context.LifeEvents.Update(lifeEvent);
            return _context.SaveChanges() > 0 ? true : false;
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
