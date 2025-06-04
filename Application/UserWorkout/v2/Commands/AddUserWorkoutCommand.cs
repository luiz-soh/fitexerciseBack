using Application.UserWorkout.v2.Boundaries;
using Application.UserWorkout.v2.Commands.Validation;
using Domain.Base.Messages;

namespace Application.UserWorkout.v2.Commands
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