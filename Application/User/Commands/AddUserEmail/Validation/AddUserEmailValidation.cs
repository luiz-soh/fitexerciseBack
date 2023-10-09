using Application.User.Boundaries.Input;
using FluentValidation;

namespace Application.User.Commands.AddUserEmail.Validation
{
    public class AddUserEmailValidation : AbstractValidator<AddUserEmailInput>
    {
        public AddUserEmailValidation() 
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("E-mail é obrigatório");

            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Email inválido");

            RuleFor(x => x.UserId)
                .GreaterThan(0)
                .WithMessage("Usuario é obrigatório");
        }
    }
}
