using MinhaSaudeFeminina.Models.Catalogs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinhaSaudeFeminina.Models.Relations
{
    public class TagGender
    {
        [Key]
        public int TagGenderId { get; set; }

        [ForeignKey("Tag")]
        public int TagId { get; set; }
        public Tag Tag { get; set; } = null!;
        
        [ForeignKey("Gender")]
        public int GenderId { get; set; }
        public Gender Gender { get; set; } = null!;
    }
}
