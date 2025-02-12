using Application.Gym.Boundaries;
using Application.User.Boundaries.Output;
using Application.User.Commands.GetUsersByGym.Validation;
using Domain.Base.Messages;

namespace Application.User.Commands.GetUsersByGym
{
    public class GetUsersByGymCommand(PaginatedInput input) : Command<PaginatedUsersOutput>
    {
        public PaginatedInput Input { get; set; } = input;

        public override bool IsValid()
        {
            ValidationResult = new GetUsersByGymValidation().Validate(Input);
            return ValidationResult.IsValid;
        }
    }
}
