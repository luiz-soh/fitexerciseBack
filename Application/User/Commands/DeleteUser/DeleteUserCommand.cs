using Application.User.Commands.DeleteUser.Validation;
using Domain.Base.Messages;

namespace Application.User.Commands.DeleteUser
{

    public class DeleteUserCommand : Command<bool>
    {
        public DeleteUserCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
        public override bool IsValid()
        {
            ValidationResult = new DeleteUserValidation().Validate(Id);
            return ValidationResult.IsValid;
        }
    }
}
