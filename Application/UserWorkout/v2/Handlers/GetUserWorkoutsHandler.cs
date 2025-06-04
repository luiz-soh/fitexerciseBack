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
    public class GetUserWorkoutsHandler : IRequestHandler<GetUserWorkoutsCommand, List<DynamoUserWorkoutOutput>>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IUserWorkoutUseCase _useCase;

        public GetUserWorkoutsHandler(IUserWorkoutUseCase useCase, IMediatorHandler handler)
        {
            _useCase = useCase;
            _mediatorHandler = handler;
        }
        public async Task<List<DynamoUserWorkoutOutput>> Handle(GetUserWorkoutsCommand request, CancellationToken cancellationToken)
        {
            if (request.IsValid())
            {
                try
                {
                    var dto = await _useCase.GetUserWorkouts(request.UserId, request.GroupId);
                    return [.. dto.Select(x => x.MapTo<DynamoUserWorkoutDto, DynamoUserWorkoutOutput>())];
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

            return [];
        }
    }
}