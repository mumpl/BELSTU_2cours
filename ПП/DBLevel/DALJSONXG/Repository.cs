using GREPO;

namespace DALJSONXG
{
    public class Repository : IRepository<WSref, Comment>
    {
        private readonly Context _context;
        private bool _disposed = false;

        public Repository(string fileName)
        {
            _context = new Context(fileName);
        }

        public List<WSref> getAllWSRef()
        {
            return _context.WSRefs;
        }

        public List<Comment> getAllComment()
        {
            return _context.Comments;
        }

        public Comment? GetCommentById(int Id)
        {
            return _context.Comments.FirstOrDefault(c => c.Id == Id);
        }

        public bool addWSRef(WSref wsRef)
        {
            if (_context.AddWSRef(wsRef))
            {
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool addComment(Comment comment)
        {
            bool result = false;

            if (_context.AddComment(comment))
            {
                _context.SaveChanges();
                result = true;
            }

            return result;
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _context.Dispose();
                _disposed = true;
            }
        }
    }
}
