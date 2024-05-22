using Application.GroupWorkout.Boundaries;
using Application.GroupWorkout.Commands.GetGroups.Validation;
using Domain.Base.Messages;

namespace Application.GroupWorkout.Commands
{
    public class GetGroupsCommand : Command<List<GroupWorkoutOutput>>
    {
        public GetGroupsCommand(int userId)
        {
            UserId = userId;
        }
        public int UserId { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new GetGroupsValidation().Validate(UserId);
            return ValidationResult.IsValid;
        }
    }
}