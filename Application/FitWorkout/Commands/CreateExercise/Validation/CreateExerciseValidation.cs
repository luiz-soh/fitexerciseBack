using Application.S3.Boundaries;
using FluentValidation;

namespace Application.FitWorkout.Commands.CreateExercise.Validation
{
    public class CreateExerciseValidation : AbstractValidator<UploadInput>
    {
        public CreateExerciseValidation()
        {
            RuleFor(x => x.ExerciseName)
                .NotEmpty()
                .NotNull()
                .WithMessage("Nome do exercicio é obrigatório");

            RuleFor(x => x.File)
                .NotNull()
                .WithMessage("O video é obrigatório");

            RuleFor(x => x.Img)
                .NotNull()
                .WithMessage("Imagem é obrigatório");
            
            RuleFor(x => x.Type)
                .NotNull()
                .WithMessage("Tipo do exercicio é obrigatório");
        }
    }
}