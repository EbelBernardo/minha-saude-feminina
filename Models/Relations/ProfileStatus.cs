using MinhaSaudeFeminina.Models.Catalogs;
using MinhaSaudeFeminina.Models.UserProfile;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinhaSaudeFeminina.Models.Relations
{
    public class ProfileStatus
    {
        [ForeignKey("Profile")]
        public int ProfileId { get; set; }
        public Profile Profile { get; set; } = null!;

        [ForeignKey("Status")]
        public int StatusId { get; set; }
        public Status Status { get; set; } = null!;
    }
}
