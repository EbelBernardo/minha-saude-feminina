using MinhaSaudeFeminina.DTOs.Users;
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
        public UserResponseDto? User { get; set; }
    }
}
