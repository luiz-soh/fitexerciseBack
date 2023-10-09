using Application.User.Boundaries.Input;
using Application.User.Commands.SignIn.Validation;
using Domain.Base.Messages;
using Domain.DTOs.Token;

namespace Application.User.Commands.SignIn
{
    public class SignInCommand : Command<TokenDto>
    {
        public SignInCommand(SignInInput input) 
        {
            Input = input;
        }

        public SignInInput Input { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new SignInValidation().Validate(Input);
            return ValidationResult.IsValid;
        }
    }
}
