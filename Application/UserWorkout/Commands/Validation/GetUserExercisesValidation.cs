using FluentValidation;

namespace Application.UserWorkout.Commands.Validation
{
    public class GetUserExercisesValidation : AbstractValidator<GetUserExercisesCommand>
    {
        public GetUserExercisesValidation()
        {
            RuleFor(x => x.UserId)
            .NotNull().WithMessage("É preciso informar o userId")
            .GreaterThan(0).WithMessage("É preciso informar o userId");

            RuleFor(x => x.GroupId)
            .NotNull().WithMessage("É preciso informar o grupo")
            .GreaterThan(0).WithMessage("É preciso informar o grupo");
        }
    }
}