
namespace REPO
{
    public interface IRepository : IDisposable
    {
        List<WSRef> GetAllWSRef();
        List<Comment> GetAllComment();
        Comment? GetCommentById(int id);
        bool AddWSRef( WSRef wSRef );
        bool AddComment(Comment comment);

    }
}
