using MinhaSaudeFeminina.Models.Relations;
using MinhaSaudeFeminina.Models.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace MinhaSaudeFeminina.Models.UserProfile
{
    public class Profile
    {
        public Profile()
        {
            ProfileGenders = new List<ProfileGender>();
            ProfileStatuses = new List<ProfileStatus>();
            ProfileObjectives = new List<ProfileObjective>();
            ProfileSymptoms = new List<ProfileSymptom>();
        }

        [Key]
        public int ProfileId { get; set; }

        [Required]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; } = null!;



        [Required(ErrorMessage = "A data de nascimento é obrigatória")]
        [DataType(DataType.Date, ErrorMessage = "A data de nascimento deve ser uma data válida")]
        [CustomValidation(typeof(Profile), nameof(ValidateDateOfBirth))]
        public DateTime DateOfBirth { get; set; }

        [NotMapped]
        public int Age
        {
            get
            {
                var today = DateTime.Today;
                var age = today.Year - DateOfBirth.Year;
                if (DateOfBirth.Date > today.AddYears(-age)) age--;
                    return age;
            }
        }

        [Required(ErrorMessage = "O termo de consentimento é obrigatório")]
        public bool Term { get; set; }


        public ICollection<ProfileGender> ProfileGenders { get; set; }
        public ICollection<ProfileStatus> ProfileStatuses { get; set; }
        public ICollection<ProfileObjective> ProfileObjectives { get; set; }
        public ICollection<ProfileSymptom> ProfileSymptoms { get; set; }
 
        
        public static ValidationResult ValidateDateOfBirth(DateTime dateofbirth, ValidationContext context)
        {
            var today = DateTime.Today;
            var age = today.Year - dateofbirth.Year;
            if (dateofbirth.Date > today.AddYears(-age)) age--;

            if (age < 0 || age > 120)
                return new ValidationResult("A idade deve estar entre 0 e 120 anos.");

            return ValidationResult.Success!;
        }
    }
}
