using Application.UserWorkout.v2.Boundaries;
using FluentValidation;

namespace Application.UserWorkout.v2.Commands.Validation
{
    public class AddUserWorkoutValidation : AbstractValidator<AddUserWorkoutInput>
    {
        public AddUserWorkoutValidation()
        {
            RuleFor(x => x.GroupId)
            .NotNull().WithMessage("Id do grupo de treino é obrigatório")
            .GreaterThan(0).WithMessage("Id do grupo de treino é obrigatório");

            RuleFor(x => x.Workout.WorkoutName)
            .NotNull().WithMessage("Nome do treino é obrigatório")
            .NotEmpty().WithMessage("Nome do treino é obrigatório");
        }
    }
}