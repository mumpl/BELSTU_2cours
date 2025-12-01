using System.ComponentModel.DataAnnotations;

namespace LinguaBender.Models
{
    public class Course
    {
        [Key]
        public string Course_Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty; //базовый, средний, продвинутый
        public int Lessons { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Price { get; set; }
        public string Language { get; set; } = string.Empty; //азиатский, европейский, арабский
        
    }
}
