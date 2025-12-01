
namespace DAL21
{
    public interface IRepository: IDisposable
    {
        List<WSRef>    getAllWSRef();
        List<Comment>  getAllComment();
        Comment?       GetCommentById(int Id);
        bool           addWSRef(WSRef wsRef);
        bool addComment(Comment comment);
    }
    public class Repository : IRepository
    {
        Context context;    
        private Repository() {this.context = new Context();}   
        public static IRepository Create()   { return new Repository();}
        public List<Comment> getAllComment() { return this.context.Comments.ToList<Comment>();}
        public List<WSRef> getAllWSRef()     { return this.context.WSRefs.ToList<WSRef>();}
        public bool addWSRef(WSRef wsref)
        {
            bool rc = false;     
             context.Database.BeginTransaction();
                context.WSRefs.Add(wsref);
                rc = ( context.SaveChanges() > 0 );
             context.Database.CommitTransaction();
             return rc;
        }
        public bool addComment(Comment comment)
        {
            bool rc = false;

            var wsRef = context.WSRefs.Find(comment.WSrefId);

            if (wsRef != null && context.Comments != null)
            {
                using var transaction = context.Database.BeginTransaction();

                context.Comments.Add(comment);
                rc = (context.SaveChanges() > 0);

                transaction.Commit();
            }

            return rc; // Возвращаем результат в конце метода
        }

        public void Dispose() {this.context.Dispose(); }

        public Comment? GetCommentById(int Id)
        {
            return this.context.Comments.FirstOrDefault<Comment>((c)=>c.Id == Id);
        }
    }


}
