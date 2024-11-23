using Application.User.Boundaries.Input;
using Application.User.Commands.RecoverPassword.Validation;
using Domain.Base.Messages;

namespace Application.User.Commands.RecoverPassword
{

    public class RecoverPasswordCommand(RecoverPasswordInput input) : Command<bool>
    {
        public RecoverPasswordInput Input { get; set; } = input;
        public override bool IsValid()
        {
            ValidationResult = new RecoverPasswordValidation().Validate(Input);
            return ValidationResult.IsValid;
        }
    }
}
