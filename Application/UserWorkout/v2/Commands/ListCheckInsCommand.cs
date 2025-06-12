using Application.UserWorkout.v2.Boundaries;
using Application.UserWorkout.v2.Commands.Validation;
using Domain.Base.Messages;

namespace Application.UserWorkout.v2.Commands
{
    public class ListCheckInsCommand(int userId) : Command<List<CheckInWorkoutOutput>>
    {
        public int UserId { get; set; } = userId;

        public override bool IsValid()
        {
            ValidationResult = new ListCheckInValidation().Validate(UserId);
            return ValidationResult.IsValid;
        }
    }
}