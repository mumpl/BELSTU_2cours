using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace DALJSONXG
{
    public class Context : IDisposable
    {
        private readonly string _fileName;
        private List<WSref> _wsRefs;

        public Context(string fileName)
        {
            _fileName = fileName;
            if (!File.Exists(fileName))
            {
                _wsRefs = new List<WSref>();
                SaveChanges();
            }
            else
            {
                _wsRefs = Load();
            }
        }

        private List<WSref> Load()
        {
            using var fs = new FileStream(_fileName, FileMode.Open, FileAccess.Read);
            return JsonSerializer.Deserialize<List<WSref>>(fs) ?? new List<WSref>();
        }

        public int SaveChanges()
        {
            using var fs = new FileStream(_fileName, FileMode.Create, FileAccess.Write);
            JsonSerializer.Serialize(fs, _wsRefs);
            return 1; // Indicates success
        }

        public List<WSref> WSRefs => _wsRefs;

        public List<Comment> Comments
        {
            get
            {
                return _wsRefs.SelectMany(wsref => wsref.Comments).ToList();
            }
        }

        private int MaxWSRefsId()
        {
            return _wsRefs.Any() ? _wsRefs.Max(wsref => wsref.Id) : 0;
        }

        public bool AddWSRef(WSref wsref)
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

            comment.Id = MaxCommentsId() + 1;
            wsRef.Comments.Add(comment);
            return true;
        }

        public void Dispose() { }
    }
}