using Application.GroupWorkout.Boundaries;
using Application.GroupWorkout.Commands.AddCheckinWorkout;
using Application.GroupWorkout.UseCase;
using BlazMapper;
using Domain.Base.Communication;
using Domain.Base.Messages.CommonMessages.Notification;
using Domain.DTOS.GroupWorkout;
using MediatR;

namespace Application.GroupWorkout.Handlers
{
    public class AddCheckInWorkoutHandler : IRequestHandler<AddCheckInWorkoutCommand, bool>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IGroupWorkoutUseCase _useCase;

        public AddCheckInWorkoutHandler(IGroupWorkoutUseCase useCase, IMediatorHandler handler)
        {
            _useCase = useCase;
            _mediatorHandler = handler;
        }

        public async Task<bool> Handle(AddCheckInWorkoutCommand request, CancellationToken cancellationToken)
        {
            if (request.IsValid())
            {
                var dto = request.Input.MapTo<AddCheckInWorkoutInput, CheckInWorkoutDto>();
                dto.UserId = request.UserId;
                dto.CheckInDate = DateTime.Now;

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