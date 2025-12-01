using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;

namespace DAL21_JSON
{
    // Класс, представляющий контекст JSON для работы с WSRef и Comment
    public class JSONContext : IDisposable
    {
        // FileStream для работы с файлами
        FileStream fs;

        // Список объектов WSRef
        public List<WSRef> WSRefs { get; private set; }

        // Свойство для получения всех комментариев из WSRefs
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

        // Приватный конструктор для инициализации контекста JSON
        private JSONContext(string FileName)
        {
            // Проверяем, существует ли файл
            if (!File.Exists(FileName))
            {
                // Если нет, создаем новый файл и сериализуем пустой список WSRefs
                this.fs = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                JsonSerializer.SerializeAsync<List<WSRef>>(this.fs, this.WSRefs = new List<WSRef>()).Wait();
            }
            else
            {
                // Если существует, открываем файл и загружаем существующие WSRefs
                this.fs = new FileStream(FileName, FileMode.Open, FileAccess.ReadWrite);
                this.WSRefs = this.Load();
            }
        }

        // Статический метод для создания нового JSONContext
        public static JSONContext Create(string FileName)
        {
            return new JSONContext(FileName);
        }

        // Метод для загрузки WSRefs из файла
        private List<WSRef> Load()
        {
            this.fs.Seek(0, SeekOrigin.Begin);
            List<WSRef>? wsrefs = JsonSerializer.DeserializeAsync<List<WSRef>?>(fs).Result;
            return (wsrefs == null) ? new List<WSRef>() : wsrefs;
        }

        // Метод для сохранения изменений в файле
        public int SaveChanges() // Сериализация
        {
            // Перемещение указателя на начало файла, ЧИТАЕМ ФАЙЛ С НАЧАЛА
            this.fs.Seek(0, SeekOrigin.Begin);
            JsonSerializer.SerializeAsync<List<WSRef>>(this.fs, this.WSRefs == null ? new List<WSRef>() : this.WSRefs);
            return 1; // Возвращаем 1 для указания на успех
        }

        // Метод для нахождения максимального ID WSRef
        private int MaxWSRefsId()
        {
            int rc = 0;
            if (this.WSRefs.Count > 0)
                rc = this.WSRefs.Max(wsref => wsref.Id);
            return rc;
        }

        // Метод для добавления WSRef в список
        public bool addWSRef(WSRef wsref)
        {
            wsref.Id = MaxWSRefsId() + 1; // Присваиваем новый ID
            this.WSRefs.Add(wsref);
            return true; // Указываем на успех
        }

        // Метод для нахождения максимального ID комментария среди всех WSRefs
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

        // Метод для добавления комментария к WSRef
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

            return rc; // Возвращаем, был ли комментарий успешно добавлен
        }

        // Реализация IDisposable для освобождения ресурсов
        public void Dispose()
        {
            fs.Dispose(); // Освобождаем FileStream
        }
    }
}