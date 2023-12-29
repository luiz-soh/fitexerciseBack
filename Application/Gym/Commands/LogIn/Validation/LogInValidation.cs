using Application.Gym.Boundaries;
using FluentValidation;

namespace Application.Gym.Commands.LogIn.Validation
{
    public class LogInValidation : AbstractValidator<LoginInput>
    {
        public LogInValidation()
        {
            RuleFor(x => x.Login)
                .NotEmpty()
                .WithMessage("Login é obrigatório");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Senha é obrigatório");
        }
    }
}