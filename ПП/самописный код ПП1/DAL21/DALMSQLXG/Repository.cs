using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using GREPO;

namespace DALMSQLXG
{
    public class Repository : IRepository<WSref, Comment>
    {
        private readonly DALMSQLXGContext _context;
        private bool _disposed = false;

        public Repository(DALMSQLXGContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public List<WSref> getAllWSRef()
        {
            return _context.WSrefs.ToList();
        }

        public List<Comment> getAllComment()
        {
            return _context.Comments.ToList();
        }

        public Comment? GetCommentById(int Id)
        {
            return _context.Comments.FirstOrDefault(c => c.Id == Id);
        }

        public bool addWSRef(WSref wsRef)
        {
            if (wsRef == null) return false;
            try
            {
                _context.WSrefs.Add(wsRef);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool addComment(Comment comment)
        {
            bool result = false;

            if (comment != null)
            {
                try
                {
                    _context.Comments.Add(comment);
                    _context.SaveChanges();
                    result = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Возникла ошибка при добавлении: {ex.Message}");
                }
            }

            return result;
        }
        

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }
    }
}