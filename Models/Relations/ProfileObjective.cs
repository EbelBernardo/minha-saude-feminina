using MinhaSaudeFeminina.Models.Catalogs;
using MinhaSaudeFeminina.Models.UserProfile;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinhaSaudeFeminina.Models.Relations
{
    public class ProfileObjective
    {
        [ForeignKey("Profile")]
        public int ProfileId { get; set; }
        public Profile Profile { get; set; } = null!;

        [ForeignKey("Objective")]
        public int ObjectiveId { get; set; }
        public Objective Objective { get; set; } = null!;
    }
}
