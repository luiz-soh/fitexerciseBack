using FluentValidation;

namespace Application.User.Commands.GetUsersByGym.Validation
{
    public class GetUsersByGymValidation : AbstractValidator<int>
    {
        public GetUsersByGymValidation()
        {
            RuleFor(i => i)
                .GreaterThan(0)
                .WithMessage("Academia obrigatório");
        }
    }
}
