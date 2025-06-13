using Application.UserWorkout.v2.Boundaries;
using Application.UserWorkout.v2.Commands.Validation;
using Domain.Base.Messages;

namespace Application.UserWorkout.v2.Commands
{
    public class AddCheckInWorkoutCommand(AddCheckInWorkoutInput input, int userId) : Command<bool>
    {
        public AddCheckInWorkoutInput Input { get; set; } = input;
        public int UserId { get; set; } = userId;

        public override bool IsValid()
        {
            ValidationResult = new AddCheckInWorkoutValidation().Validate(Input);
            return ValidationResult.IsValid;
        }
    }
}