using Application.FitWorkout.Boundaries;
using Application.FitWorkout.Commands.GetExercises.Validation;
using Domain.Base.Messages;

namespace Application.FitWorkout.Commands.GetExercises
{
    public class GetExecisesCommand(int id) : Command<List<ExerciseOutput>>
    {
        public int Id { get; set; } = id;
        public override bool IsValid()
        {
            ValidationResult = new GetExercisesValidation().Validate(Id);
            return ValidationResult.IsValid;
        }
    }
}