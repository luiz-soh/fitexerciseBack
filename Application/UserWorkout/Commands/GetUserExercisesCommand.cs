using Application.UserWorkout.Boundaries;
using Application.UserWorkout.Commands.Validation;
using Domain.Base.Messages;

namespace Application.UserWorkout.Commands
{
    public class GetUserExercisesCommand(int groupId, int userId) : Command<List<UserExerciseOutput>>
    {

        public int UserId { get; set; } = userId;
        public int GroupId { get; set; } = groupId;

        public override bool IsValid()
        {
            ValidationResult = new GetUserExercisesValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}