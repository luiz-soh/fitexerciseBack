using Application.UserWorkout.Boundaries;
using Application.UserWorkout.Commands.Validation;
using Domain.Base.Messages;

namespace Application.UserWorkout.Commands
{
    public class AddUserWorkoutCommand(AddUserWorkoutInput input) : Command<bool>
    {
        public AddUserWorkoutInput Input { get; set; } = input;

        public override bool IsValid()
        {
            ValidationResult = new AddUserWorkoutValidation().Validate(Input);
            return ValidationResult.IsValid;
        }
    }
}