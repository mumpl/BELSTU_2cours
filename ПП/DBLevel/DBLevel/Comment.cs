using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DBLevel
{
    public class Comment
    {
        [Key]
        public int? Id                { get; set; }
        public string? Commtext { get; set; }
        [ForeignKey("WSRef")]
        public int? WSrefId           { get; set; }
        public DateTime? Stamp        { get; set; }
        
    }
}
