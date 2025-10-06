using MinhaSaudeFeminina.Models.Relations;
using System.ComponentModel.DataAnnotations;

namespace MinhaSaudeFeminina.Models.Catalogs
{
    public class Tag
    {
        public Tag()
        {
            TagGenders = new List<TagGender>();
            TagStatuses = new List<TagStatus>();
            TagObjectives = new List<TagObjective>();
            TagSymptoms = new List<TagSymptom>();
        }

        [Key]
        public int TagId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;


        public ICollection<TagGender> TagGenders { get; set; }
        public ICollection<TagStatus> TagStatuses { get; set; }
        public ICollection<TagObjective> TagObjectives { get; set; }
        public ICollection<TagSymptom> TagSymptoms { get; set; }
    }
}
