using Application.UserWorkout.Boundaries;
using Application.UserWorkout.Commands.Validation;
using Domain.Base.Messages;

namespace Application.UserWorkout.Commands
{
    public class UpdateUserWorkoutCommand(UpdateUserWorkoutInputOld input) : Command<bool>
    {
        public UpdateUserWorkoutInputOld Input { get; set; } = input;

        public override bool IsValid()
        {
            ValidationResult = new UpdateUserWorkoutValidation().Validate(Input);
            return ValidationResult.IsValid;
        }
    }
}