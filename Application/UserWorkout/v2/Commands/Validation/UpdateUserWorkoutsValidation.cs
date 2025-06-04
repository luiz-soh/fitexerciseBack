using Application.UserWorkout.v2.Boundaries;
using FluentValidation;

namespace Application.UserWorkout.v2.Commands.Validation
{
    public class UpdateUserWorkoutsValidation : AbstractValidator<UpdateUserWorkoutsInput>
    {
        public UpdateUserWorkoutsValidation()
        {
            RuleFor(x => x.GroupId)
            .NotNull().WithMessage("Id do grupo de treino é obrigatório")
            .GreaterThan(0).WithMessage("Id do grupo de treino é obrigatório");

            RuleFor(x => x.Workouts)
            .NotNull().WithMessage("É obrigatório informar os exercicios");

            RuleForEach(x => x.Workouts).ChildRules(order =>
            {
                order.RuleFor(x => x.WorkoutName)
                .NotNull().WithMessage("Nome do treino {CollectionIndex} é obrigatório")
                .NotEmpty().WithMessage("Nome do treino {CollectionIndex} é obrigatório");
            });
        }
    }
}