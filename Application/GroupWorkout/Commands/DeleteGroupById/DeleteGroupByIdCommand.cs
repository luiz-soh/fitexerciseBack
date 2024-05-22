using Application.GroupWorkout.Commands.DeleteGroupById.Validation;
using Domain.Base.Messages;

namespace Application.GroupWorkout.Commands.DeleteGroupById
{
    public class DeleteGroupByIdCommand : Command<bool>
    {
        public DeleteGroupByIdCommand(int id, int userId)
        {
            Id = id;
            UserId = userId;
        }

        public int Id { get; set; }
        public int UserId { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new DeleteGroupByIdValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}