using Application.User.Boundaries.Input;
using Application.User.Commands.SignUp.Validation;
using Domain.Base.Messages;

namespace Application.User.Commands.SignUp
{
    public class SignUpCommand(SignUpInput input) : Command<bool>
    {
        public SignUpInput Input { get; set; } = input;

        public override bool IsValid()
        {
            ValidationResult = new SignUpValidation().Validate(Input);
            return ValidationResult.IsValid;
        }
    }
}
