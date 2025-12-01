using System.Text.Json;
using REPO;

namespace DALJSONX
{
    public class Context : IDisposable
    {
        private readonly string _fileName;
        private List<WSRef> _wsRefs;

        private Context(string fileName)
        {
            _fileName = fileName;
            if (!File.Exists(fileName))
            {
                _wsRefs = new List<WSRef>();
                SaveChanges();
            }
            else
            {
                _wsRefs = Load();
            }
        }

        public static Context Create(string fileName)
        {
            return new Context(fileName);
        }

        private List<WSRef> Load()
        {
            using var fs = new FileStream(_fileName, FileMode.Open, FileAccess.Read);
            return JsonSerializer.Deserialize<List<WSRef>>(fs) ?? new List<WSRef>();
        }

        public int SaveChanges()
        {
            using var fs = new FileStream(_fileName, FileMode.Create, FileAccess.Write);
            JsonSerializer.Serialize(fs, _wsRefs);
            return 1; 
        }

        public List<WSRef> WSRefs => _wsRefs;

        public List<Comment> Comments
        {
            get
            {
                return _wsRefs.SelectMany(wsref => wsref.Comments ?? new List<Comment>()).ToList();
            }
        }

        private int MaxWSRefsId()
        {
            return _wsRefs.Any() ? _wsRefs.Max(wsref => wsref.Id) : 0;
        }

        public bool AddWSRef(WSRef wsref)
        {
            wsref.Id = MaxWSRefsId() + 1;
            _wsRefs.Add(wsref);
            return true;
        }

        private int MaxCommentsId()
        {
            return Comments.Any() ? Comments.Max(comment => comment.Id) : 0;
        }

        public bool AddComment(Comment comment)
        {
            var wsRef = _wsRefs.FirstOrDefault(w => w.Id == comment.WSrefId);
            if (wsRef == null)
                return false;

            wsRef.Comments ??= new List<Comment>();
            comment.Id = MaxCommentsId() + 1;
            wsRef.Comments.Add(comment);
            return true;
        }

        public void Dispose()
        {
        }
    }
}
