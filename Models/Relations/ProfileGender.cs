using MinhaSaudeFeminina.Models.Catalogs;
using MinhaSaudeFeminina.Models.UserProfile;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinhaSaudeFeminina.Models.Relations
{
    public class ProfileGender
    {
        [ForeignKey("Profile")]
        public int ProfileId { get; set; }
        public Profile Profile { get; set; } = null!;

        [ForeignKey("Gender")]
        public int GenderId { get; set; }
        public Gender Gender { get; set; } = null!;
    }
}
