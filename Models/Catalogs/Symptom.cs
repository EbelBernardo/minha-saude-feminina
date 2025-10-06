using MinhaSaudeFeminina.Models.Relations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinhaSaudeFeminina.Models.Catalogs
{
    public class Symptom
    {
        public Symptom()
        {
            TagSymptoms = new List<TagSymptom>();
            ProfileSymptoms = new List<ProfileSymptom>();
        }

        [Key]
        public int SymptomId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;


        public ICollection<TagSymptom> TagSymptoms { get; set; }
        public ICollection<ProfileSymptom> ProfileSymptoms { get; set; }
    }
}
