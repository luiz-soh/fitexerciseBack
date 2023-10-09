using Application.User.Boundaries.Input;
using Application.User.Commands.AddUserEmail.Validation;
using Domain.Base.Messages;

namespace Application.User.Commands.AddUserEmail
{
    public class AddUserEmailCommand : Command<bool>
    {
        public AddUserEmailCommand(AddUserEmailInput input)
        {
            Input = input;
        }

        public AddUserEmailInput Input { get; set; }
        public override bool IsValid()
        {
            ValidationResult = new AddUserEmailValidation().Validate(Input);
            return ValidationResult.IsValid;
        }
    }
}
