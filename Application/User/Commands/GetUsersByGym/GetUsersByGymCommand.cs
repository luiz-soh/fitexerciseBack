using Application.User.Commands.GetUserData.Validation;
using Domain.Base.Messages;
using Domain.DTOs.User;

namespace Application.User.Commands.GetUsersByGym
{
    public class GetUsersByGymCommand : Command<List<UserDto>>
    {
        public GetUsersByGymCommand(int gymId)
        {
            GymId = gymId;
        }

        public int GymId { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new GetUserDataValidation().Validate(GymId);
            return ValidationResult.IsValid;
        }
    }
}
