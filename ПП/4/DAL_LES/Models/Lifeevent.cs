using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL_LES.Models
{
    public class LifeEvent // Событие в жизни знаменитости
    {
        public LifeEvent()
        {
            this.Description = "";
        }
        [Key]
        public int Id { get; set; } // Id События
        [ForeignKey("Celebrity")]
        public int CelebrityId { get; set; } // Id Знаменитости
        public DateTime Date { get; set; } // дата События
        public string Description { get; set; } // описание События
        public string? ReqPhotoPath { get; set; } // request path Фотографии
        public Celebrity Celebrity { get; set; }
    }
}
