using Application.UserWorkout.Boundaries;
using Application.UserWorkout.Commands.Validation;
using Domain.Base.Messages;

namespace Application.UserWorkout.Commands
{
    public class AddUserWorkoutCommandOld(AddUserWorkoutInputOld input) : Command<bool>
    {
        public AddUserWorkoutInputOld Input { get; set; } = input;

        public override bool IsValid()
        {
            ValidationResult = new AddUserWorkoutValidationOld().Validate(Input);
            return ValidationResult.IsValid;
        }
    }
}