using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace DAL21
{
    public class Comment
    {
        [Key]
        public int   Id          { get; set; }
        public DateTime? Stamp  { get; set; }
        public string?   Commtext { get; set; }
        [ForeignKey("WSRef")]                //  для FK ->  WSRef(PK) 
        public int   WSrefId { get; set; }   
        public WSRef WSref   { get; set; }   //обозначение связи  комментарий к 1 wsref

    }
}
