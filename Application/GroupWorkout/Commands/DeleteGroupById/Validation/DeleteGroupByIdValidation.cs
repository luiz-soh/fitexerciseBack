using FluentValidation;

namespace Application.GroupWorkout.Commands.DeleteGroupById.Validation
{
    public class DeleteGroupByIdValidation : AbstractValidator<DeleteGroupByIdCommand>
    {
        public DeleteGroupByIdValidation()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .GreaterThan(0)
                .WithMessage("Id do grupo é obrigatório");

            RuleFor(x => x.UserId)
                .NotNull()
                .GreaterThan(0)
                .WithMessage("Id do usuário é obrigatório");
        }
    }
}