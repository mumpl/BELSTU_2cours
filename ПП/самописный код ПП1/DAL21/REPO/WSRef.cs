using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPO
{
    public class WSRef
    {
        [Key]
        public int Id { get; set; }
        public string? Url { get; set; }
        public string? Description { get; set; }
        public int? Plus { get; set; }
        public int? Minus { get; set; }
        public List<Comment>? Comments { get; set; } // Навигационное свойство один-ко-многим
    }
}
