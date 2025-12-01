using System;
using System.Collections.Generic;

namespace DALJSONXG
{
    public class WSref
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public int Plus { get; set; }
        public int Minus { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }

    public class Comment
    {
        public int Id { get; set; }
        public DateTime? Stamp { get; set; }
        public string Commtext { get; set; }
        public int WSrefId { get; set; }
    }
}