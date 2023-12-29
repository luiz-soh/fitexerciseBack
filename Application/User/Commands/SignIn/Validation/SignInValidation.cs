using Application.User.Boundaries.Input;
using FluentValidation;

namespace Application.User.Commands.SignIn.Validation
{
    public class SignInValidation : AbstractValidator<SignInInput>
    {
        public SignInValidation() 
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage("Username é obrigatório");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Senha é obrigatório");
        }
    }
}
