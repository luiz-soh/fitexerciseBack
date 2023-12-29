using Application.Gym.Boundaries;
using Application.Gym.Commands.CreateGym.Validation;
using Domain.Base.Messages;

namespace Application.Gym.Commands.CreateGym
{
    public class CreateGymCommand : Command<bool>
    {

        public CreateGymCommand(CreateGymInput input)
        {
            Input = input;
        }
        public CreateGymInput Input { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new CreateGymValidation().Validate(Input);
            return ValidationResult.IsValid;
        }
    }
}