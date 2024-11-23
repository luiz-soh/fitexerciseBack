using Application.User.Boundaries.Input;
using Application.User.Commands.ForgotPassword.Validation;
using Domain.Base.Messages;

namespace Application.User.Commands.ForgotPassword
{

    public class ForgotPasswordCommand(ForgotPasswordInput input) : Command<bool>
    {
        public ForgotPasswordInput Input { get; set; } = input;
        public override bool IsValid()
        {
            ValidationResult = new ForgotPasswordValidation().Validate(Input);
            return ValidationResult.IsValid;
        }
    }
}
