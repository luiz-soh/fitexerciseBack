using Application.FitWorkout.Boundaries;
using FluentValidation;

namespace Application.FitWorkout.Commands.EditExerciseData.Validation
{
    public class EditExerciseDataValidation : AbstractValidator<EditExerciseDataInput>
    {
        public EditExerciseDataValidation() 
        {
            RuleFor(x => x.WorkoutId)
                .NotEqual(0)
                .NotNull()
                .WithMessage("Id do exercicio é obrigatório");
            
            RuleFor(x => x.ExerciseName)
                .NotEmpty()
                .WithMessage("Nome do exercicio é obrigatório");

            RuleFor(x => x.Type)
                .NotNull()
                .WithMessage("Tipo do exercicio é obrigatório");
        }
    }
}
