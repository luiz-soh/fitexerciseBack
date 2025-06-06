using Application.GroupWorkout.Boundaries;
using Application.GroupWorkout.Commands.AddCheckinWorkout.Validation;
using Domain.Base.Messages;

namespace Application.GroupWorkout.Commands.AddCheckinWorkout
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