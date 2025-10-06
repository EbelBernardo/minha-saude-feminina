using System.ComponentModel.DataAnnotations;

namespace MinhaSaudeFeminina.DTOs.Tags
{
    public class TagRegisterDto
    {
        [Required(ErrorMessage = "O título é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O título não pode ter mais de 100 caracteres")]
        public string Title { get; set; } = null!;
    }
}
