using Application.UserWorkout.Commands;
using Application.UserWorkout.UseCase;
using Domain.Base.Communication;
using Domain.Base.Messages.CommonMessages.Notification;
using MediatR;

namespace Application.UserWorkout.Handlers
{
    public class ChangeUserWorkoutPositionHandler : IRequestHandler<ChangeUserWorkoutPositionCommand, bool>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IUserWorkoutUseCase _useCase;

        public ChangeUserWorkoutPositionHandler(IUserWorkoutUseCase useCase, IMediatorHandler handler)
        {
            _useCase = useCase;
            _mediatorHandler = handler;
        }

        public async Task<bool> Handle(ChangeUserWorkoutPositionCommand request, CancellationToken cancellationToken)
        {
            if (request.IsValid())
            {
                await _useCase.ChangeUserWorkoutPosition(request.Input);
                return true;
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