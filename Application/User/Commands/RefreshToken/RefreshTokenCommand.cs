using Application.User.Boundaries.Input;
using Application.User.Commands.RefreshToken.Validation;
using Domain.Base.Messages;
using Domain.DTOs.Token;

namespace Application.User.Commands.RefreshToken
{

    public class RefreshTokenCommand(UpdateTokenInput input) : Command<TokenDto>
    {
        public UpdateTokenInput Input { get; set; } = input;
        public override bool IsValid()
        {
            ValidationResult = new RefreshTokenValidation().Validate(Input);
            return ValidationResult.IsValid;
        }
    }
}