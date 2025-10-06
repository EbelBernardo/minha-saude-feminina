using Microsoft.AspNetCore.Identity;
using MinhaSaudeFeminina.Models.UserProfile;

namespace MinhaSaudeFeminina.Models.User
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string? FullName { get; set; }

        public Profile? Profile { get; set; }
    }
}
