using FluentValidation;

namespace Application.GroupWorkout.Commands.GetGroups.Validation
{
    public class GetGroupsValidation : AbstractValidator<int>
    {
        public GetGroupsValidation()
        {
            RuleFor(x => x)
                .NotNull()
                .GreaterThan(0)
                .WithMessage("UserId é obrigatório");
        }
    }
}