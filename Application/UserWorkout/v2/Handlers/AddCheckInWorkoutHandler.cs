using Application.UserWorkout.UseCase;
using Application.UserWorkout.v2.Boundaries;
using Application.UserWorkout.v2.Commands;
using BlazMapper;
using Domain.Base.Communication;
using Domain.Base.Messages.CommonMessages.Notification;
using Domain.DTOs.UserWorkout;
using MediatR;

namespace Application.UserWorkout.v2.Handlers
{
    public class AddCheckInWorkoutHandler : IRequestHandler<AddCheckInWorkoutCommand, bool>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IUserWorkoutUseCase _useCase;

        public AddCheckInWorkoutHandler(IUserWorkoutUseCase useCase, IMediatorHandler handler)
        {
            _useCase = useCase;
            _mediatorHandler = handler;
        }

        public async Task<bool> Handle(AddCheckInWorkoutCommand request, CancellationToken cancellationToken)
        {
            if (request.IsValid())
            {
                var dto = new CheckInWorkoutDto(request.Input.GroupId, request.Input.Duration, request.UserId);
                await _useCase.AddCheckinWorkout(dto);
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