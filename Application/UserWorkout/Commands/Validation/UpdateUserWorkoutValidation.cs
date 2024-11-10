using Application.UserWorkout.Boundaries;
using FluentValidation;

namespace Application.UserWorkout.Commands.Validation
{
    public class UpdateUserWorkoutValidation : AbstractValidator<UpdateUserWorkoutInput>
    {
        public UpdateUserWorkoutValidation()
        {
            RuleFor(x => x.UserId)
            .NotNull().WithMessage("UserId é obrigatório")
            .GreaterThan(0).WithMessage("UserId é obrigatório");

            RuleFor(x => x.WorkoutId)
            .NotNull().WithMessage("WorkoutId é obrigatório")
            .GreaterThan(0).WithMessage("WorkoutId é obrigatório");

            RuleFor(x => x.GroupWorkoutId)
            .NotNull().WithMessage("GroupWorkoutId é obrigatório")
            .GreaterThan(0).WithMessage("GroupWorkoutId é obrigatório");

            RuleFor(x => x.WorkoutPosition)
            .NotNull().WithMessage("WorkoutPosition é obrigatório");

            RuleFor(x => x.UwId)
            .NotNull().WithMessage("UwId é obrigatório")
            .GreaterThan(0).WithMessage("UwId é obrigatório");
        }
    }
}