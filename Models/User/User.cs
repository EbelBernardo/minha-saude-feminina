using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinhaSaudeFeminina.Models.UserProfile
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [MaxLength(100, ErrorMessage = "O nome não pode ter mais de 100 caracteres")]
        public string Name { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "O Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        [MaxLength(150, ErrorMessage = "O Email não pode ter mais de 150 caracteres")]
        public string Email { get; set; } = string.Empty;

        [NotMapped]
        [Required(ErrorMessage = "A senha é obrigatória")]
        [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres")]
        public string PasswordHash { get; set; } = string.Empty;

        public Profile? Profile { get; set; }
    }
}
