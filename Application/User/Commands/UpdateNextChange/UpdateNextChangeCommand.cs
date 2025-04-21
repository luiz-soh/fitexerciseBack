using Application.User.Boundaries.Input;
using Application.User.Commands.UpdateNextChange.Validation;
using Domain.Base.Messages;

namespace Application.User.Commands.UpdateNextChange
{
    public class UpdateNextChangeCommand(UpdateNextChangeInput input, int userId) : Command<bool>
    {
        public UpdateNextChangeInput Input { get; set; } = input;
        public int UserId { get; set; } = userId;

        public override bool IsValid()
        {
            ValidationResult = new UpdateNextChangeValidation().Validate(Input);
            return ValidationResult.IsValid;
        }
    }
}
