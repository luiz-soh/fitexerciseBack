using Application.User.Boundaries.Input;
using FluentValidation;

namespace Application.User.Commands.ForgotPassword.Validation
{

    public class ForgotPasswordValidation : AbstractValidator<ForgotPasswordInput>
    {
        public ForgotPasswordValidation()
        {
            RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("Formato de e-mail invalido")
            .NotEmpty().NotNull().WithMessage("E-mail é obrigatório");
        }
    }
}