namespace DAL_LES
{
    public class Repository : IRepository, IDisposable
    {
        private readonly Context _context;

        public Repository()
        {
            _context = new Context();
        }

        public static IRepository Create() => new Repository();

        public List<Celebrity> GetAllCelebrities()
        {
            return _context.Celebrities?.ToList() ?? new List<Celebrity>();
        }

        public Celebrity? GetCelebrityById(int Id)
        {
            return _context.Celebrities?.Find(Id);
        }

        public bool DelCelebrity(int id)
        {
            var celebrity = _context.Celebrities?.Find(id);
            if (celebrity == null) return false;

            _context.Celebrities?.Remove(celebrity);
            return _context.SaveChanges() > 0;
        }

        public bool AddCelebrity(Celebrity celebrity)
        {
            if (celebrity == null || _context.Celebrities == null)
                return false;

            _context.Celebrities.Add(celebrity);
            return _context.SaveChanges() > 0;
        }

        public bool UpdCelebrity(int id, Celebrity celebrity)
        {
            var existing = _context.Celebrities?.Find(id);
            if (existing == null) return false;

            existing.FullName = celebrity.FullName;
            existing.Nationality = celebrity.Nationality;

            return _context.SaveChanges() > 0;
        }

        public List<Lifeevent> GetAllLifeevents()
        {
            return _context.Lifeevents?.ToList() ?? new List<Lifeevent>();
        }

        public Lifeevent? GetLifeeventById(int Id)
        {
            return _context.Lifeevents?.Find(Id);
        }

        public bool DelLifeevent(int id)
        {
            var lifeevent = _context.Lifeevents?.Find(id);
            if (lifeevent == null) return false;

            _context.Lifeevents?.Remove(lifeevent);
            return _context.SaveChanges() > 0;
        }

        public bool AddLifeevent(Lifeevent lifeevent)
        {
            if (lifeevent == null || _context.Lifeevents == null)
                return false;

            _context.Lifeevents.Add(lifeevent);
            return _context.SaveChanges() > 0;
        }

        public bool UpdLifeevent(int id, Lifeevent lifeevent)
        {
            var existing = _context.Lifeevents?.Find(id);
            if (existing == null) return false;

            existing.CelebrityId = lifeevent.CelebrityId;
            existing.Date = lifeevent.Date;
            existing.Description = lifeevent.Description;

            return _context.SaveChanges() > 0;
        }

        public List<Lifeevent> GetLifeeventsByCelebrityId(int celebrityId)
        {
            return _context.Lifeevents?
                .Where(e => e.CelebrityId == celebrityId)
                .ToList() ?? new List<Lifeevent>();
        }

        public Celebrity? GetCelebrityByLifeeventId(int lifeeventId)
        {
            var lifeevent = _context.Lifeevents?.Find(lifeeventId);
            if (lifeevent == null) return null;

            return _context.Celebrities?.Find(lifeevent.CelebrityId);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }

    public class CashCelebrities : ICashCelebrities
    {
        private readonly IRepository _repository;
        private List<Celebrity> _cachedCelebrities;
        private DateTime _lastUpdateTime;
        private TimeSpan _cacheDuration = TimeSpan.FromMinutes(5);

        public CashCelebrities(IRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            UpdateCache();
        }

        public List<Celebrity> GetCachedCelebrities()
        {
            if (DateTime.Now - _lastUpdateTime > _cacheDuration)
                UpdateCache();
            return _cachedCelebrities;
        }

        public DateTime LastUpdateTime => _lastUpdateTime;
        public TimeSpan CacheDuration { get; set; } = TimeSpan.FromMinutes(5);

        public void ResetCache() => UpdateCache();

        private void UpdateCache()
        {
            _cachedCelebrities = _repository.GetAllCelebrities();
            _lastUpdateTime = DateTime.Now;
        }
    }
}