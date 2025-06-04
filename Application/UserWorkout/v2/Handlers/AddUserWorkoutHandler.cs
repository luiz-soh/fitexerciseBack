using Application.UserWorkout.Commands;
using Application.UserWorkout.UseCase;
using Application.UserWorkout.v2.Commands;
using Domain.Base.Communication;
using Domain.Base.Messages.CommonMessages.Notification;
using Domain.DTOs.UserWorkout;
using MediatR;

namespace Application.UserWorkout.v2.Handlers
{
    public class AddUserWorkoutHandler : IRequestHandler<AddUserWorkoutCommand, bool>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IUserWorkoutUseCase _useCase;

        public AddUserWorkoutHandler(IUserWorkoutUseCase useCase, IMediatorHandler handler)
        {
            _useCase = useCase;
            _mediatorHandler = handler;
        }
        public async Task<bool> Handle(AddUserWorkoutCommand request, CancellationToken cancellationToken)
        {
            if (request.IsValid())
            {
                try
                {
                    var input = request.Input.Workout;
                    var dto = new DynamoUserWorkoutDto(input.Position, input.Series, input.Repetitions, input.WorkoutId, input.WorkoutName);
                    await _useCase.SaveUserWorkout(request.Input.UserId, request.Input.GroupId, dto);
                    return true;
                }
                catch
                {
                    await _mediatorHandler.PublishNotification(new DomainNotification(request.MessageType, "Ocorreu um erro ao tentar adicionar exercicio"));
                }

            }
            else
            {
                foreach (var error in request.ValidationResult.Errors)
                {
                    await _mediatorHandler.PublishNotification(new DomainNotification(request.MessageType, error.ErrorMessage));
                }
            }

            return false;
        }
    }
}