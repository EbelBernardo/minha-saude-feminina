using FluentValidation;
using MinhaSaudeFeminina.DTOs.UserAuth;
using System.Data;

namespace MinhaSaudeFeminina.Validators
{
    public class UpdateEmailValidator : AbstractValidator<UpdateEmailDto>
    {
        public UpdateEmailValidator()
        {
            RuleFor(x => x.NewEmail)
                .NotEmpty().WithMessage("O email não pode estar vazio.")
                .EmailAddress().WithMessage("O email fornecido não é válido.");
        }
    }
}
