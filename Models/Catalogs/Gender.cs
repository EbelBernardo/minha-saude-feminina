using MinhaSaudeFeminina.Models.Relations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinhaSaudeFeminina.Models.Catalogs
{
    public class Gender
    {
        public Gender()
        {
            ProfileGenders = new List<ProfileGender>();
        }

        [Key]
        public int GenderId { get; set; }
        public string Title { get; set; } = string.Empty;


        [Required]
        [ForeignKey("Tag")]
        public int TagId { get; set; }
        [Required]
        public Tag Tag { get; set; } = null!;


        public ICollection<ProfileGender> ProfileGenders { get; set; }
    }
}
