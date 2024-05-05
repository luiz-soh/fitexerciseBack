using Application.FitWorkout.Boundaries;
using Application.FitWorkout.Commands.EditExerciseMedia.Validation;
using Domain.Base.Messages;

namespace Application.FitWorkout.Commands.EditExerciseMedia
{
    public class EditExerciseMediaCommand : Command<bool>
    {
        public EditExerciseMediaCommand(EditExerciseMediaInput input)
        {
            Input = input;
        }
        public EditExerciseMediaInput Input { get; set; }
        public override bool IsValid()
        {
            ValidationResult = new EditExerciseMediaValidation().Validate(Input);
            return ValidationResult.IsValid;
        }
    }
}