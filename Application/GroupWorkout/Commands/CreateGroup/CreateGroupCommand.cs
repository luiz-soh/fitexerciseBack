using Application.GroupWorkout.Boundaries;
using Domain.Base.Messages;

namespace Application.GroupWorkout.Commands.CreateGroup
{
    public class CreateGroupCommand : Command<bool>
    {
        public CreateGroupCommand(CreateGroupInput input, int userId)
        {
            Input = input;
            UserId = userId;
        }
        public CreateGroupInput Input { get; set; }
        public int UserId { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new CreateGroupValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}