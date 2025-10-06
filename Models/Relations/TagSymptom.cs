using MinhaSaudeFeminina.Models.Catalogs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinhaSaudeFeminina.Models.Relations
{
    public class TagSymptom
    {
        [ForeignKey("Tag")]
        public int TagId { get; set; }
        public Tag Tag { get; set; } = null!;

        [ForeignKey("Symptom")]
        public int SymptomId { get; set; }
        public Symptom Symptom { get; set; } = null!;
    }
}
