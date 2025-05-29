using Application.UserWorkout.v2.Boundaries;
using Application.UserWorkout.v2.Commands.Validation;
using Domain.Base.Messages;

namespace Application.UserWorkout.v2.Commands
{
    public class UpdateUserWorkoutsCommand(UpdateUserWorkoutsInput input) : Command<bool>
    {
        public UpdateUserWorkoutsInput Input { get; set; } = input;

        public override bool IsValid()
        {
            ValidationResult = new UpdateUserWorkoutsValidation().Validate(Input);
            return ValidationResult.IsValid;
        }
    }
}