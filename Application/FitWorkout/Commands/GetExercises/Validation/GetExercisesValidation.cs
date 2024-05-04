using FluentValidation;

namespace Application.FitWorkout.Commands.GetExercises.Validation
{
    public class GetExercisesValidation : AbstractValidator<int>
    {
        public GetExercisesValidation() 
        {
            RuleFor(x => x)
                .NotEqual(0)
                .NotNull()
                .WithMessage("UserId é obrigatório");
        }
    }
}
