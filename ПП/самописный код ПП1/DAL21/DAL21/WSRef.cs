using System.ComponentModel.DataAnnotations;

namespace DAL21
{    
        public class WSRef
        {
            [Key]
            public int     Id               { get; set; } //первичный ключ
            public string? Url              { get; set; }
            public string? Description      { get; set; }
            public int?    Plus             { get; set; }
            public int?    Minus            { get; set; }
            public List<Comment>? Comments { get; set; } // для FK, обозначение связи один ко многим

        }
}