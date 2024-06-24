using Application.UserWorkout.Boundaries;
using FluentValidation;

namespace Application.UserWorkout.Commands.Validation
{
    public class ChangeUserWorkoutPositionValidation : AbstractValidator<List<UserWorkoutPositionInput>>
    {
        public ChangeUserWorkoutPositionValidation()
        {
            RuleForEach(x => x).ChildRules(rule =>
            {
                rule.RuleFor(x => x.Position).NotNull().WithMessage("É obrigatório informar a posição");

                rule.RuleFor(x => x.UserWorkoutId)
                .NotNull().WithMessage("Exercicio é obrigatório")
                .GreaterThan(0).WithMessage("Exercicio é obrigatório");
            });
        }
    }
}