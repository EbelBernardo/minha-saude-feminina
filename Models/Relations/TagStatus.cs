using MinhaSaudeFeminina.Models.Catalogs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinhaSaudeFeminina.Models.Relations
{
    public class TagStatus
    {
        [Key]
        public int TagStatusId { get; set; }

        [ForeignKey("Tag")]
        public int TagId { get; set; }
        public Tag Tag { get; set; } = null!;

        [ForeignKey("Status")]
        public int StatusId { get; set; }
        public Status Status{ get; set; } = null!;
    }
}
