using FluentValidation;
using MinhaSaudeFeminina.DTOs.Common;
using MinhaSaudeFeminina.DTOs.Gender;

namespace MinhaSaudeFeminina.Validators
{
    public class TitleAndTagDtoValidator<T> : AbstractValidator<T> where T : IHaveTitleAndTagDto
    {
        public TitleAndTagDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("O título é obrigatório.")
                .MaximumLength(50).WithMessage("O título não pode ter mais de 50 caracteres.");

            RuleFor(x => x.TagId)
                .NotNull().WithMessage("A Tag é obrigatória.")
                .GreaterThan(0).WithMessage("A Tag deve ser maior que zero.");
        }
    }

    public class TitleAndTagsDtoValidator<T> : AbstractValidator<T> where T : IHaveTitleAndTagsDto
    {
        public TitleAndTagsDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("O título é obrigatório.")
                .MaximumLength(50).WithMessage("O título não pode ter mais de 50 caracteres.");

            RuleFor(x => x.TagId)
            .Must(list => list != null && list.Any() && list.All(id => id > 0))
            .WithMessage("A Tag é obrigatória.");
        }
    }
}
