using Application.Gym.Boundaries;
using FluentValidation;

namespace Application.Gym.Commands.CreateGym.Validation
{
    public class CreateGymValidation : AbstractValidator<CreateGymInput>
    {
        public CreateGymValidation()
        {
            RuleFor(x => x.GymName)
            .NotEmpty()
            .WithMessage("Nome da academia é obrigatório");

            RuleFor(x => x.PlanId)
            .GreaterThan(0)
            .WithMessage("é obrigatório escolher um plano");

            RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Senha é obrigatório");

            RuleFor(x => x.Password)
                .MinimumLength(8)
                .NotEmpty()
                .Matches(@"[][""!@$%^&*(){}:;<>,.?/+_=|'~\\-]")
                .WithMessage("Deve ter pelo menos 8 caracteres e 1 caractere especial");

            RuleFor(x => x.Email)
            .NotEmpty()
                .EmailAddress()
                .WithMessage("formato de e-mail inválido");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .Equal(x => x.Password)
                .WithMessage("senhas não batem");
            
            RuleFor(x => x.Login)
                .NotEmpty()
                .WithMessage("Login é obrigatório");
        }
    }
}