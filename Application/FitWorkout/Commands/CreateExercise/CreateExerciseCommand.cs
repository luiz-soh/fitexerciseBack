using Application.FitWorkout.Commands.CreateExercise.Validation;
using Application.S3.Boundaries;
using Domain.Base.Messages;

namespace Application.FitWorkout.Commands.CreateExercise
{
    public class CreateExerciseCommand : Command<bool>
    {
        public CreateExerciseCommand(UploadInput upload)
        {
            Upload = upload;
        }
        public UploadInput Upload { get; set; }
        public override bool IsValid()
        {
            ValidationResult = new CreateExerciseValidation().Validate(Upload);
            return ValidationResult.IsValid;
        }
    }
}