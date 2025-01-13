using Application.FitWorkout.Boundaries;
using Application.FitWorkout.Commands.GetGymExercises.Validation;
using Application.Gym.Boundaries;
using Domain.Base.Messages;

namespace Application.FitWorkout.Commands.GetGymExercises
{
    public class GetGymExecisesCommand(PaginatedInput input) : Command<PaginatedExercisesOutput>
    {
        public PaginatedInput Input { get; set; } = input;
        public override bool IsValid()
        {
            ValidationResult = new GetGymExercisesValidation().Validate(Input);
            return ValidationResult.IsValid;
        }
    }
}