using Microsoft.AspNetCore.Identity;
using MinhaSaudeFeminina.Models.UserProfile;
using System.ComponentModel.DataAnnotations;

namespace MinhaSaudeFeminina.Models.User
{
    public class ApplicationUser : IdentityUser<int>
    {
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        public Profile? Profile { get; set; }
    }
}
