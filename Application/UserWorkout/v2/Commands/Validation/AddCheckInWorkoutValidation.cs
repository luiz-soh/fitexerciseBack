using Application.UserWorkout.v2.Boundaries;
using FluentValidation;

namespace Application.UserWorkout.v2.Commands.Validation
{
    public class AddCheckInWorkoutValidation : AbstractValidator<AddCheckInWorkoutInput>
    {
        public AddCheckInWorkoutValidation()
        {
            RuleFor(x => x.Duration)
                .NotEmpty()
                .WithMessage("É obrigatório informar a duração do treino");
            
            RuleFor(x => x.GroupId)
                .NotNull()
                .GreaterThan(0)
                .WithMessage("É obrigatório informar o grupo");
        }
    }
}