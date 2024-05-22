using Application.GroupWorkout.Boundaries;
using Application.GroupWorkout.Commands.GetGroupById.Validation;
using Domain.Base.Messages;

namespace Application.GroupWorkout.Commands.GetGroupById
{
    public class GetGroupByIdCommand : Command<GroupWorkoutOutput>
    {
        public GetGroupByIdCommand(int id)
        {
            Id = id;
        }
        public int Id { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new GetGroupByIdValidation().Validate(Id);
            return ValidationResult.IsValid;
        }
    }
}