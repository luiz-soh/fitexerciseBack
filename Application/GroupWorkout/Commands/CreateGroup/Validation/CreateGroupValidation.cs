using Application.GroupWorkout.Boundaries;
using FluentValidation;

namespace Application.GroupWorkout.Commands.CreateGroup
{
    public class CreateGroupValidation : AbstractValidator<CreateGroupCommand>
    {
        public CreateGroupValidation()
        {
            RuleFor(x => x.Input.Name)
                .NotEmpty()
                .WithMessage("Nome do grupo é obrigatório");
            
            RuleFor(x => x.UserId)
                .NotNull()
                .GreaterThan(0)
                .WithMessage("Usuario é obrigatório");
        }
    }
}