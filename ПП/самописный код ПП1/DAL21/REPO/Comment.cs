using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPO
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public DateTime? Stamp { get; set; }
        public string? Commtext { get; set; }

        [ForeignKey("WSRef")]
        public int WSrefId { get; set; }
        public WSRef WSref { get; set; } // Навигационное свойство , ссылаемся к одному WSref
    }
}
