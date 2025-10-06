using MinhaSaudeFeminina.Models.Relations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinhaSaudeFeminina.Models.Catalogs
{
    public class Status
    {
        public Status()
        {
            ProfileStatuses = new List<ProfileStatus>();
        }

        [Key]
        public int StatusId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;


        [Required]
        [ForeignKey("Tag")]
        public int TagId { get; set; }
        [Required]
        public Tag Tag { get; set; } = null!;


        public ICollection<ProfileStatus> ProfileStatuses { get; set; }
    }
}
