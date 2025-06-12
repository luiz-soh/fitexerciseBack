using Application.UserWorkout.Commands;
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
    public class ListCheckInsHandler : IRequestHandler<ListCheckInsCommand, List<CheckInWorkoutOutput>>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IUserWorkoutUseCase _useCase;

        public ListCheckInsHandler(IUserWorkoutUseCase useCase, IMediatorHandler handler)
        {
            _useCase = useCase;
            _mediatorHandler = handler;
        }
        public async Task<List<CheckInWorkoutOutput>> Handle(ListCheckInsCommand request, CancellationToken cancellationToken)
        {
            if (request.IsValid())
            {
                try
                {
                    var dto = await _useCase.ListCheckinsWorkout(request.UserId);
                    return [.. dto.Select(x => x.MapTo<CheckInWorkoutDto, CheckInWorkoutOutput>())];
                }
                catch
                {
                    await _mediatorHandler.PublishNotification(new DomainNotification(request.MessageType, "Ocorreu um erro ao buscar os Check-ins"));
                }

            }
            else
            {
                foreach (var error in request.ValidationResult.Errors)
                {
                    await _mediatorHandler.PublishNotification(new DomainNotification(request.MessageType, error.ErrorMessage));
                }
            }

            return [];
        }
    }
}