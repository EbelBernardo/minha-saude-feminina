using MinhaSaudeFeminina.Models.User;
using MinhaSaudeFeminina.Models.UserProfile;

namespace MinhaSaudeFeminina.DTOs.Profiles
{
    public class ProfileResponseDto
    {
        public int ProfileId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age { get; set; }
        public bool Term { get; set; }

        public int UserId { get; set; }
        public ApplicationUser? User { get; set; }
        //UserResponseDto

        public List<string> Genders { get; set; } = new();
        public List<string> Statuses { get; set; } = new();
        public List<string> Objectives { get; set; } = new();
        public List<string> Symptoms { get; set; } = new();
    }
}
