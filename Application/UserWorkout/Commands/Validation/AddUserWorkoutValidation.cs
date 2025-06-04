using Application.UserWorkout.Boundaries;
using FluentValidation;

namespace Application.UserWorkout.Commands.Validation
{
    public class AddUserWorkoutValidationOld : AbstractValidator<AddUserWorkoutInputOld>
    {
        public AddUserWorkoutValidationOld()
        {
            RuleFor(x => x.UserId)
            .NotNull().WithMessage("Id do usuário é obrigatório")
            .GreaterThan(0).WithMessage("Id do usuário é obrigatório");

            RuleFor(x => x.WorkoutId)
            .NotNull().WithMessage("Id do exercicio é obrigatório")
            .GreaterThan(0).WithMessage("Id do exercicio é obrigatório");

            RuleFor(x => x.GroupWorkoutId)
            .NotNull().WithMessage("Id do grupo de treino é obrigatório")
            .GreaterThan(0).WithMessage("Id do grupo de treino é obrigatório");
        }
    }
}