using Application.User.Commands.DeleteUser.Validation;
using Application.UserWorkout.Commands.Validation;
using Domain.Base.Messages;

namespace Application.UserWorkout.Commands
{
    public class DeleteUserWorkoutCommand(int userId, int workoutId) : Command<bool>
    {
        public int UserId { get; set; } = userId;
        public int WorkoutId { get; set; } = workoutId;

        public override bool IsValid()
        {
            ValidationResult = new DeleteUserWorkoutValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}