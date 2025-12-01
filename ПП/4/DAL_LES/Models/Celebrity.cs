
using DAL_LES.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL_LES.Models
{
    public class Celebrity // Знаменитость
    {
        public Celebrity()
        {
            this.FullName = "";
            this.Nationality = "XX";
        }
        [Key]
        public int Id { get; set; } // Id Знаменитости
        public string FullName { get; set; } // полное имя Знаменитости
        public string Nationality { get; set; } // гражданство Знаменитости
        public string? ReqPhotoPath { get; set; } // request path Фотографии
        [InverseProperty("Celebrity")]
        public List<LifeEvent> Lifeevents { get; set; }
    }
}
