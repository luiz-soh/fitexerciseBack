using Application.User.Boundaries.Input;
using FluentValidation;

namespace Application.User.Commands.RefreshToken.Validation
{

    public class RefreshTokenValidation : AbstractValidator<UpdateTokenInput>
    {
        public RefreshTokenValidation()
        {
            RuleFor(x => x.UserId)
            .NotNull()
            .NotEqual(0)
            .WithMessage("Id do usuario é obrigatório");

            RuleFor(x => x.RefreshToken)
            .NotNull()
            .NotEmpty()
            .WithMessage("Refresh Token é obrgatório");
        }
    }
}