using Application.Gym.Boundaries;
using Application.Gym.Commands.LogIn.Validation;
using Domain.Base.Messages;
using Domain.DTOs.Gym;

namespace Application.Gym.Commands.LogIn
{
    public class LogInCommand : Command<GymTokenDto>
    {

        public LogInCommand(LoginInput input)
        {
            Input = input;
        }
        public LoginInput Input { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new LogInValidation().Validate(Input);
            return ValidationResult.IsValid;
        }
    }
}