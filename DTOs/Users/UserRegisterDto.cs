using System.ComponentModel.DataAnnotations;

namespace MinhaSaudeFeminina.DTOs.Users
{
    public class UserRegisterDto
    {

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O nome não pode ter mais de 100 caracteres")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "O Email é obrigatório")]
        [EmailAddress(ErrorMessage = "O Email deve ser um endereço de email válido")]
        [MaxLength(150, ErrorMessage = "O Email não pode ter mais de 150 caracteres")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "A senha é obrigatória")]
        [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres")]
        public string PasswordHash { get; set; } = null!;
    }
}
