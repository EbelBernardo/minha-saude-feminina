using MinhaSaudeFeminina.DTOs.Gender;
using MinhaSaudeFeminina.DTOs.Objectives;
using MinhaSaudeFeminina.DTOs.Statuses;
using MinhaSaudeFeminina.DTOs.Symptoms;

namespace MinhaSaudeFeminina.Validators
{
    public class RegisterValidators
    {
        public class RegisterGenderValidator : TitleAndTagDtoValidator<GenderRegisterDto> { }

        public class RegisterObjectiveValidator : TitleAndTagDtoValidator<ObjectiveRegisterDto> { }

        public class RegisterStatusValidator : TitleAndTagDtoValidator<StatusRegisterDto> { }

        public class RegisterSymptomValidator : TitleAndTagsDtoValidator<SymptomRegisterDto> { }
    }
}
