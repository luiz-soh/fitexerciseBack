using FluentValidation;

namespace Application.FitWorkout.Commands.GetExerciseById.Validation
{
    public class GetExerciseByIdValidation : AbstractValidator<int>
    {
        public GetExerciseByIdValidation() 
        {
            RuleFor(x => x)
                .NotEqual(0)
                .NotNull()
                .WithMessage("Id do exercicio é obrigatório");
        }
    }
}
