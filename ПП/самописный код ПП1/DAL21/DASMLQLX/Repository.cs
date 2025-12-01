// Repository.cs
using REPO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DALMSQLX
{
    public class Repository : IRepository
    {
        private readonly Context _context;
        private bool _disposed = false;

        private Repository()
        {
            _context = new Context();
        }

        public static IRepository Create()
        {
            return new Repository();
        }

        public List<WSRef> GetAllWSRef()
        {
            return _context.WSRefs.ToList();
        }

        public List<Comment> GetAllComment()
        {
            return _context.Comments.ToList();
        }

        public Comment? GetCommentById(int id)
        {
            return _context.Comments.FirstOrDefault(c => c.Id == id);
        }

        public bool AddWSRef(WSRef wsRef)
        {
            try
            {
                using var transaction = _context.Database.BeginTransaction();
                _context.WSRefs.Add(wsRef);
                bool result = _context.SaveChanges() > 0;
                transaction.Commit();
                return result;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool AddComment(Comment comment)
        {
            bool result = false;

            try
            {
                var wsRef = _context.WSRefs.Find(comment.WSrefId);

                if (wsRef != null)
                {
                    using var transaction = _context.Database.BeginTransaction();
                    _context.Comments.Add(comment);
                    result = _context.SaveChanges() > 0;
                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка в добавлении комментария: {ex.Message}");
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
            GC.SuppressFinalize(this);
        }
    }
}