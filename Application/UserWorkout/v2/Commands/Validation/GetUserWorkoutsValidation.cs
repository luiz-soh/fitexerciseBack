using FluentValidation;

namespace Application.UserWorkout.v2.Commands.Validation
{
    public class GetUserWorkoutsValidation : AbstractValidator<GetUserWorkoutsCommand>
    {
        public GetUserWorkoutsValidation()
        {
            RuleFor(x => x.GroupId)
            .NotNull().WithMessage("Id do grupo de treino é obrigatório")
            .GreaterThan(0).WithMessage("Id do grupo de treino é obrigatório");
        }
    }
}