using FluentValidation;

namespace Application.FitWorkout.Commands.GetGymExercises.Validation
{
    public class GetGymExercisesValidation : AbstractValidator<int>
    {
        public GetGymExercisesValidation() 
        {
            RuleFor(x => x)
                .NotEqual(0)
                .NotNull()
                .WithMessage("UserId é obrigatório");
        }
    }
}
