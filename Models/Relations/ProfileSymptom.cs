using MinhaSaudeFeminina.Models.Catalogs;
using MinhaSaudeFeminina.Models.UserProfile;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinhaSaudeFeminina.Models.Relations
{
    public class ProfileSymptom
    {
        [ForeignKey("Profile")]
        public int ProfileId { get; set; }
        public Profile Profile { get; set; } = null!;

        [ForeignKey("Symptom")]
        public int SymptomId { get; set; }
        public Symptom Symptom { get; set; } = null!;
    }
}
