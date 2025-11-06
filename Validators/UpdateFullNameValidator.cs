using FluentValidation;
using MinhaSaudeFeminina.DTOs.UserAuth;

namespace MinhaSaudeFeminina.Validators
{
    public class UpdateFullNameValidator : AbstractValidator<UpdateFullNameDto>
    {
        public UpdateFullNameValidator()
        {
            RuleFor(x => x.NewFullName)
                .NotEmpty().WithMessage("O nome completo não pode estar vazio.")
                .MaximumLength(100).WithMessage("O nome completo não pode exceder 100 caracteres.")
                .MinimumLength(2).WithMessage("O nome completo deve ter pelo menos 2 caracteres.");
        }
    }
}
