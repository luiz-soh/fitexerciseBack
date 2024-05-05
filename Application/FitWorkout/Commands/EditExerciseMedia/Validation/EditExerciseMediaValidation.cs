using Application.FitWorkout.Boundaries;
using FluentValidation;

namespace Application.FitWorkout.Commands.EditExerciseMedia.Validation
{
    public class EditExerciseMediaValidation : AbstractValidator<EditExerciseMediaInput>
    {
        public EditExerciseMediaValidation()
        {
            RuleFor(x => x.WorkoutId)
                .NotEqual(0)
                .NotNull()
                .WithMessage("Id do exercicio é obrigatório");

            RuleFor(x => x.Video)
                .NotNull()
                .WithMessage("Video é obrigatório");

            RuleFor(x => x.Img)
                .NotNull()
                .WithMessage("Imagem é obrigatório");
        }
    }
}
