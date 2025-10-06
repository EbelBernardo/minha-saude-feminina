using MinhaSaudeFeminina.Models.Relations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinhaSaudeFeminina.Models.Catalogs
{
    public class Objective
    {
        public Objective()
        {
            ProfileObjectives = new List<ProfileObjective>();
        }

        [Key]
        public int ObjectiveId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;


        [Required]
        [ForeignKey("Tag")]
        public int TagId { get; set; }
        [Required]
        public Tag Tag { get; set; } = null!;


        public ICollection<ProfileObjective> ProfileObjectives { get; set; }
    }
}
