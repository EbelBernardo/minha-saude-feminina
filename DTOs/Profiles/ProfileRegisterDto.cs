using MinhaSaudeFeminina.Models.Catalogs;
using MyGender = MinhaSaudeFeminina.Models.Catalogs.Gender;
using System.ComponentModel.DataAnnotations;

namespace MinhaSaudeFeminina.DTOs.Profiles
{
    public class ProfileRegisterDto
    {
        [Required(ErrorMessage ="A data de nascimento é obrigatória")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "O termo de consentimento é obrigatório")]
        public bool Term { get; set; }

        //public List<MyGender>? Genders { get; set; }
        //public List<Objective>? Objectives { get; set; }
        //public List<Status>? Statuses { get; set; }
        //public List<Symptom>? Symtoms { get; set; }
    }
}
