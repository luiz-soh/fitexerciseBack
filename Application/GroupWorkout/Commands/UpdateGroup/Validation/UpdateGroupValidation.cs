using Application.GroupWorkout.Boundaries;
using FluentValidation;

namespace Application.GroupWorkout.Commands.UpdateGroup.Validation
{
    public class UpdateGroupValidation : AbstractValidator<UpdateGroupInput>
    {
        public UpdateGroupValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Nome do grupo é obrigatório");

            RuleFor(x => x.Id)
                .NotNull()
                .GreaterThan(0)
                .WithMessage("Id do grupo é obrigatório");
        }
    }
}