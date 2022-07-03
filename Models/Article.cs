using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.ComponentModel.DataAnnotations;
namespace EFIntegrated.Models
{
    public class Article
    {
        [Key]
        public int ID { get; set; }
        [StringLength(255, MinimumLength = 5)]
        [Required]
        [Column(TypeName = "nvarchar")]
        public string Title { get; set; }

        [DataType(DataType.DateTime)]
        [Required]
        public DateTime PublishDate { get; set; }

        [Column(TypeName = "ntext")]
        public string Content {set; get;}
    }
}
