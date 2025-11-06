using MinhaSaudeFeminina.Models.Catalogs;
using MyGender = MinhaSaudeFeminina.Models.Catalogs.Gender;
using System.ComponentModel.DataAnnotations;
using MinhaSaudeFeminina.DTOs.Objectives;
using MinhaSaudeFeminina.DTOs.Statuses;
using MinhaSaudeFeminina.DTOs.Symptoms;
using MinhaSaudeFeminina.DTOs.Gender;

namespace MinhaSaudeFeminina.DTOs.Profiles
{
    public class ProfileRegisterDto
    {
        [Required(ErrorMessage ="A data de nascimento é obrigatória")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "O termo de consentimento é obrigatório")]
        public bool Term { get; set; }

        public List<string> Genders { get; set; } = new();

        public List<string> Objectives { get; set; } = new();

        public List<string> Statuses { get; set; } = new();

        public List<string> Symptoms { get; set; } = new();

    }
}
