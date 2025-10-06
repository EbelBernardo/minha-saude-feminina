﻿using System.ComponentModel.DataAnnotations;

namespace MinhaSaudeFeminina.DTOs.Symptoms
{
    public class SymptomRegisterDto
    {
        [Required(ErrorMessage = "O título é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O título não pode ter mais de 100 caracteres")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "O Id da tag é obrigatório.")]
        public List<int> TagId { get; set; } = null!;
    }
}
