using FluentValidation;
using MinhaSaudeFeminina.DTOs.UserAuth;

namespace MinhaSaudeFeminina.Validators
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O email é obrigatório.")
                .EmailAddress().WithMessage("O email fornecido não é válido.");

            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("O nome completo é obrigatório.")
                .MinimumLength(2).WithMessage("O nome completo deve ter no mínimo 2 caracteres.");
        }
    }
}
