using FluentValidation;

namespace Application.User.Commands.DeleteUser.Validation
{

    public class DeleteUserValidation : AbstractValidator<int>
    {
        public DeleteUserValidation()
        {
            RuleFor(x => x)
            .NotNull()
            .WithMessage("Id do usuario é obrigatório");
        }
    }
}