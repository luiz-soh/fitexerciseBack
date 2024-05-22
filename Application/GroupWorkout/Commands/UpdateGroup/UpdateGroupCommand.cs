using Application.GroupWorkout.Boundaries;
using Application.GroupWorkout.Commands.UpdateGroup.Validation;
using Domain.Base.Messages;

namespace Application.GroupWorkout.Commands.UpdateGroup
{
    public class UpdateGroupCommand : Command<bool>
    {
        public UpdateGroupCommand(UpdateGroupInput input)
        {
            Input = input;
        }
        public UpdateGroupInput Input { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new UpdateGroupValidation().Validate(Input);
            return ValidationResult.IsValid;
        }
    }
}