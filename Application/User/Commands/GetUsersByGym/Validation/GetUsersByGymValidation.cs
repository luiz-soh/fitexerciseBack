using Application.Gym.Boundaries;
using FluentValidation;

namespace Application.User.Commands.GetUsersByGym.Validation
{
    public class GetUsersByGymValidation : AbstractValidator<PaginatedInput>
    {
        public GetUsersByGymValidation()
        {
            RuleFor(i => i.GymId)
                .GreaterThan(0)
                .WithMessage("Academia obrigatório");
        }
    }
}
