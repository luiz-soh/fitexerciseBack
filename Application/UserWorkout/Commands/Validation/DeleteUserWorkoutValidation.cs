using FluentValidation;

namespace Application.UserWorkout.Commands.Validation
{
    public class DeleteUserWorkoutValidation : AbstractValidator<DeleteUserWorkoutCommand>
    {
        public DeleteUserWorkoutValidation()
        {
            RuleFor(x => x.UserId)
            .NotNull().WithMessage("Id do usuário é obrigatório")
            .GreaterThan(0).WithMessage("Id do usuário é obrigatório");

            RuleFor(x => x.WorkoutId)
            .NotNull().WithMessage("Id do exercicio é obrigatório")
            .GreaterThan(0).WithMessage("Id do exercicio é obrigatório");

        }
    }
}