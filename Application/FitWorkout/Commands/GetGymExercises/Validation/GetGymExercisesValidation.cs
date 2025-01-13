using Application.Gym.Boundaries;
using FluentValidation;

namespace Application.FitWorkout.Commands.GetGymExercises.Validation
{
    public class GetGymExercisesValidation : AbstractValidator<PaginatedInput>
    {
        public GetGymExercisesValidation() 
        {
            RuleFor(x => x.GymId)
                .NotEqual(0)
                .NotNull()
                .WithMessage("UserId é obrigatório");
        }
    }
}
