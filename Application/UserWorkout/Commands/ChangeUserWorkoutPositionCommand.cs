using Application.UserWorkout.Boundaries;
using Application.UserWorkout.Commands.Validation;
using Domain.Base.Messages;

namespace Application.UserWorkout.Commands
{
    public class ChangeUserWorkoutPositionCommand(List<UserWorkoutPositionInput> input) : Command<bool>
    {
        public List<UserWorkoutPositionInput> Input { get; set; } = input;

        public override bool IsValid()
        {
            ValidationResult = new ChangeUserWorkoutPositionValidation().Validate(Input);
            return ValidationResult.IsValid;
        }
    }
}