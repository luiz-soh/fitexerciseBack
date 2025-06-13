using FluentValidation;

namespace Application.UserWorkout.v2.Commands.Validation
{
    public class ListCheckInValidation : AbstractValidator<int>
    {
        public ListCheckInValidation()
        {
            RuleFor(x => x)
            .NotNull()
            .GreaterThanOrEqualTo(0).WithMessage("Id do usuário é obrigatório");
        }
    }
}