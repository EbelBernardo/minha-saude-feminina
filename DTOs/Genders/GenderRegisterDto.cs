using MinhaSaudeFeminina.DTOs.Common;
using System.ComponentModel.DataAnnotations;

namespace MinhaSaudeFeminina.DTOs.Gender
{
    public class GenderRegisterDto : IHaveTitleAndTagDto
    {
        [Required(ErrorMessage = "O título é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O título não pode ter mais de 100 caracteres")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "O Id da tag é obrigatório.")]
        public int TagId { get; set; }
    }
}
