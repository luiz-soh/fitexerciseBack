using Application.User.Boundaries.Input;
using FluentValidation;

namespace Application.User.Commands.SignUp.Validation
{
    public class SignUpValidation : AbstractValidator<SignUpInput>
    {
        public SignUpValidation() 
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage("Username é obrigatório");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Senha é obrigatório");

            RuleFor(x => x.Password)
                .MinimumLength(8)
                .NotEmpty()
                .Matches(@"[][""!@$%^&*(){}:;<>,.?/+_=|'~\\-]")
                .WithMessage("Deve ter pelo menos 8 caracteres e 1 caractere especial");

            RuleFor(x => x.UserEmail)
                .EmailAddress().When(x => !string.IsNullOrEmpty(x.UserEmail))
                .WithMessage("formato de e-mail inválido");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .Equal(x => x.Password)
                .WithMessage("senhas não batem");
        }
    }
}