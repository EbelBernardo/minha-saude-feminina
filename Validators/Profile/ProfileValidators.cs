using FluentValidation;
using MinhaSaudeFeminina.DTOs.Profiles;

namespace MinhaSaudeFeminina.Validators.Profile
{
    public class ProfileValidators : AbstractValidator<ProfileRegisterDto>
    {
        public ProfileValidators()
        {
            RuleSet("Create", () =>
            {
                RuleFor(x => x.DateOfBirth)
                    .LessThan(DateTime.Today).WithMessage("A data de nascimento deve ser no passado.")
                    .NotEmpty().WithMessage("A data de nascimento é obrigatória.");

                RuleFor(x => x.Term)
                    .Equal(true).WithMessage("O termo de consentimento deve ser aceito.");

                RuleFor(x => x.Genders)
                    .NotNull().WithMessage("Os gêneros são obrigatórios.")
                    .Must(list => list != null && list.Any()).WithMessage("Deve haver pelo menos um gênero selecionado.");

                RuleFor(x => x.Objectives)
                    .NotNull().WithMessage("Os objetivos são obrigatórios.")
                    .Must(list => list != null && list.Any()).WithMessage("Deve haver pelo menos um objetivo selecionado.");

                RuleFor(x => x.Statuses)
                    .NotNull().WithMessage("Os status são obrigatórios.")
                    .Must(list => list != null && list.Any()).WithMessage("Deve haver pelo menos um status selecionado.");

                RuleFor(x => x.Symptoms)
                    .NotNull().WithMessage("Os sintomas são obrigatórios.")
                    .Must(list => list != null && list.Any()).WithMessage("Deve haver pelo menos um sintoma selecionado.");
            });

            RuleSet("Update", () =>
            {
                RuleFor(x => x.DateOfBirth)
                    .LessThan(DateTime.Today).WithMessage("A data de nascimento deve ser no passado.")
                    .NotEmpty().WithMessage("A data de nascimento é obrigatória.");
            });
        }
    }
}
