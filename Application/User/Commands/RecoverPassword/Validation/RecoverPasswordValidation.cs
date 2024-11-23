using Application.User.Boundaries.Input;
using FluentValidation;

namespace Application.User.Commands.RecoverPassword.Validation
{

    public class RecoverPasswordValidation : AbstractValidator<RecoverPasswordInput>
    {
        public RecoverPasswordValidation()
        {
            RuleFor(x => x.RecoverCode)
            .NotEmpty()
            .WithMessage("Codigo de recuperação é obrigatório");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Senha é obrigatório");

            RuleFor(x => x.Password)
                .MinimumLength(8)
                .NotEmpty()
                .Matches(@"[][""!@$%^&*(){}:;<>,.?/+_=|'~\\-]")
                .WithMessage("Deve ter pelo menos 8 caracteres e 1 caractere especial");
        }
    }
}