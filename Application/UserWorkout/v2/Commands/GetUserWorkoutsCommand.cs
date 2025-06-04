using Application.UserWorkout.v2.Boundaries;
using Application.UserWorkout.v2.Commands.Validation;
using Domain.Base.Messages;

namespace Application.UserWorkout.v2.Commands
{
    public class GetUserWorkoutsCommand(int groupId, int userId) : Command<List<DynamoUserWorkoutOutput>>
    {
        public int GroupId { get; set; } = groupId;
        public int UserId { get; set; } = userId;

        public override bool IsValid()
        {
            ValidationResult = new GetUserWorkoutsValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}