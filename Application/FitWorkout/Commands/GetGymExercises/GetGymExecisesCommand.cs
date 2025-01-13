using Application.FitWorkout.Boundaries;
using Application.FitWorkout.Commands.GetGymExercises.Validation;
using Domain.Base.Messages;

namespace Application.FitWorkout.Commands.GetGymExercises
{
    public class GetGymExecisesCommand(int id) : Command<List<ExerciseOutput>>
    {
        public int Id { get; set; } = id;
        public override bool IsValid()
        {
            ValidationResult = new GetGymExercisesValidation().Validate(Id);
            return ValidationResult.IsValid;
        }
    }
}