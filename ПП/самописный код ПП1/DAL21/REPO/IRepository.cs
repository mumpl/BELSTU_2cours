using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPO
{
    public interface IRepository : IDisposable
    {
        List<WSRef> GetAllWSRef();
        List<Comment> GetAllComment();
        Comment? GetCommentById(int id);
        bool AddWSRef(WSRef wsRef);
        bool AddComment(Comment comment);
    }
}
