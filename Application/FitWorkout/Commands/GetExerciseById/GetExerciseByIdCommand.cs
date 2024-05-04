using Application.FitWorkout.Boundaries;
using Application.FitWorkout.Commands.GetExerciseById.Validation;
using Domain.Base.Messages;

namespace Application.FitWorkout.Commands.GetExerciseById
{
    public class GetExerciseByIdCommand(int id) : Command<FullExerciseOutput>
    {
        public int Id { get; set; } = id;
        public override bool IsValid()
        {
            ValidationResult = new GetExerciseByIdValidation().Validate(Id);
            return ValidationResult.IsValid;
        }
    }
}