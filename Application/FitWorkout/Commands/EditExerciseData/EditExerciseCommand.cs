using Application.FitWorkout.Boundaries;
using Application.FitWorkout.Commands.EditExerciseData.Validation;
using Domain.Base.Messages;

namespace Application.FitWorkout.Commands.EditExerciseData
{
    public class EditExerciseDataCommand : Command<bool>
    {
        public EditExerciseDataCommand(EditExerciseDataInput input)
        {
            Input = input;
        }
        public EditExerciseDataInput Input { get; set; }
        public override bool IsValid()
        {
            ValidationResult = new EditExerciseDataValidation().Validate(Input);
            return ValidationResult.IsValid;
        }
    }
}