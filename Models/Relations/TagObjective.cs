using MinhaSaudeFeminina.Models.Catalogs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinhaSaudeFeminina.Models.Relations
{
    public class TagObjective
    {
        [Key]
        public int TagObjectiveId { get; set; }

        [ForeignKey("Tag")]
        public int TagId { get; set; }
        public Tag Tag { get; set; } = null!;

        [ForeignKey("Objective")]
        public int ObjectiveId { get; set; }
        public Objective Objective { get; set; } = null!;
    }
}
