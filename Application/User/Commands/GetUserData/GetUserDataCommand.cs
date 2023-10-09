using Application.User.Commands.GetUserData.Validation;
using Domain.Base.Messages;
using Domain.DTOs.User;

namespace Application.User.Commands.GetUserData
{
    public class GetUserDataCommand : Command<UserDto>
    {
        public GetUserDataCommand(int userId) 
        {
            UserId = userId;
        }

        public int UserId {  get; set; }

        public override bool IsValid()
        {
            ValidationResult = new GetUserDataValidation().Validate(UserId);
            return ValidationResult.IsValid;
        }
    }
}
