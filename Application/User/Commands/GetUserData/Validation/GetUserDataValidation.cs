using FluentValidation;

namespace Application.User.Commands.GetUserData.Validation
{
    public class GetUserDataValidation : AbstractValidator<int>
    {
        public GetUserDataValidation()
        {
            RuleFor(i => i)
                .GreaterThan(0)
                .WithMessage("Usuário obrigatório");
        }
    }
}
