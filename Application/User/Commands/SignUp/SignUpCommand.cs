using Application.User.Boundaries.Input;
using Application.User.Commands.SignUp.Validation;
using Domain.Base.Messages;
using Domain.DTOs.Authentication;

namespace Application.User.Commands.SignUp
{
    public class SignUpCommand : Command<bool>
    {
        public SignUpCommand(SignUpInput input) 
        {
            Input = input;
        }

        public SignUpInput Input { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new SignUpValidation().Validate(Input);
            return ValidationResult.IsValid;
        }
    }
}
