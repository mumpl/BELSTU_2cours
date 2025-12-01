using REPO;

namespace DALJSONX
{
    public class Repository : IRepository
    {
        private readonly Context _context;
        private bool _disposed = false;

        private Repository(string fileName)
        {
            _context = Context.Create(fileName);
        }

        public static IRepository Create(string fileName)
        {
            return new Repository(fileName);
        }

        public List<WSRef> GetAllWSRef()
        {
            return _context.WSRefs;
        }

        public List<Comment> GetAllComment()
        {
            return _context.Comments;
        }

        public Comment? GetCommentById(int id)
        {
            return _context.Comments.FirstOrDefault(c => c.Id == id);
        }

        public bool AddWSRef(WSRef wsRef)
        {
            bool result = false;

            if (_context.AddWSRef(wsRef))
            {
                _context.SaveChanges();
                result = true;
            }

            return result;
        }

        public bool AddComment(Comment comment)
        {
            if (_context.AddComment(comment))
            {
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _context.Dispose();
                _disposed = true;
            }
            GC.SuppressFinalize(this);
        }
    }
}
