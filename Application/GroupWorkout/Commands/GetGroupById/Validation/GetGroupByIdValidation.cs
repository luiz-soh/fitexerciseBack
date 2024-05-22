using FluentValidation;

namespace Application.GroupWorkout.Commands.GetGroupById.Validation
{
    public class GetGroupByIdValidation : AbstractValidator<int>
    {
        public GetGroupByIdValidation()
        {
            RuleFor(x => x)
                .NotNull()
                .GreaterThan(0)
                .WithMessage("Id do grupo é obrigatório");
        }
    }
}