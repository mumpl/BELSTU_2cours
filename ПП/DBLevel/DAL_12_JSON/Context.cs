using System.Text.Json;

namespace DAL_12_JSON
{
    // Класс, представляющий контекст JSON для работы с WSRef и Comment
    public class JSONContext : IDisposable
    {
        FileStream fs;
        public List<WSRef> WSRefs { get; private set; }

        public List<Comment> Comments
        {
            get
            {
                List<Comment> rc = new List<Comment>();
                // Объединяем список комментариев из каждого WSRef
                this.WSRefs.ForEach(wsref => { wsref.Comments?.ForEach(comment => rc.Add(comment)); });
                return rc;
            }
        }

        //конструктор для инициализации контекста JSON
        private JSONContext(string FileName)
        {
            if (!File.Exists(FileName))
            {
                //сериализация пустого списка WSRefs
                this.fs = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                JsonSerializer.SerializeAsync<List<WSRef>>(this.fs, this.WSRefs = new List<WSRef>()).Wait();
            }
            else
            {
                //загружаем существующие WSRefs
                this.fs = new FileStream(FileName, FileMode.Open, FileAccess.ReadWrite);
                this.WSRefs = this.Load();
            }
        }
        //создание нового JSONContext
        public static JSONContext Create(string FileName) 
        { 
            return new JSONContext(FileName); 
        }
        //загрузка WSRefs из файла
        private List<WSRef> Load()
        {
            this.fs.Seek(0, SeekOrigin.Begin);
            List<WSRef>? wsrefs = JsonSerializer.DeserializeAsync<List<WSRef>?>(fs).Result;
            return (wsrefs == null) ? new List<WSRef>() : wsrefs;
        }
        public int SaveChanges()
        {
            //читаем файл с начала
            this.fs.Seek(0, SeekOrigin.Begin);
            JsonSerializer.SerializeAsync<List<WSRef>>(this.fs, this.WSRefs == null ? new List<WSRef>() : this.WSRefs);
            return 1;
        }
        private int MaxWSRefsId()
        {
            int rc = 0;
            if (this.WSRefs.Count > 0) 
                rc = this.WSRefs.Max(wsref => wsref.Id);
            return rc;
        }
        public bool addWSRef(WSRef wsref)
        {
            wsref.Id = MaxWSRefsId() + 1;
            this.WSRefs.Add(wsref);
            return true;
        }
        private int MaxCommentsId()
        {
            int rc = 0;

            this.WSRefs.ForEach(wsref => {
                if (wsref.Comments != null && wsref.Comments.Any())
                {
                    int? m = wsref.Comments.Max(comment => comment.Id);
                    if (m.HasValue && m > rc)
                    {
                        rc = (int)m;
                    }
                }
            });

            return rc;
        }
        public bool addComment(Comment comment)
        {
            bool rc = false;
            int idx = this.WSRefs.FindIndex(wsref => wsref.Id == comment.WSrefId);

            if (rc = (idx >= 0))
            {
                // Убедитесь, что список комментариев инициализирован
                if (this.WSRefs[idx].Comments == null)
                {
                    this.WSRefs[idx].Comments = new List<Comment>(); // Инициализируем, если null
                }

                comment.Id = this.MaxCommentsId() + 1; // Присваиваем новый ID
                this.WSRefs[idx].Comments.Add(comment); // Добавляем комментарий
            }

            return rc;
        }
        public void Dispose() 
        { 
            fs.Dispose(); 
        }
    }
}
