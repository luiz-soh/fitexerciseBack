using Application.User.Boundaries.Input;
using FluentValidation;

namespace Application.User.Commands.UpdateNextChange.Validation
{
    public class UpdateNextChangeValidation : AbstractValidator<UpdateNextChangeInput>
    {
        public UpdateNextChangeValidation()
        {
            RuleFor(x => x.NextChange)
                .NotEmpty()
                .WithMessage("É obrigatório preencher uma data");
        }
    }
}