using Application.GroupWorkout.Boundaries;
using Application.GroupWorkout.Commands;
using Application.GroupWorkout.UseCase;
using Domain.Base.Communication;
using Domain.Base.Messages.CommonMessages.Notification;
using MediatR;

namespace Application.GroupWorkout.Handlers
{
    public class GetGroupsHandler : IRequestHandler<GetGroupsCommand, List<GroupWorkoutOutput>>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IGroupWorkoutUseCase _useCase;

        public GetGroupsHandler(IGroupWorkoutUseCase useCase, IMediatorHandler handler)
        {
            _useCase = useCase;
            _mediatorHandler = handler;
        }

        public async Task<List<GroupWorkoutOutput>> Handle(GetGroupsCommand request, CancellationToken cancellationToken)
        {
            if (request.IsValid())
            {
                var groups = await _useCase.GetGroups(request.UserId);

                return groups.Select(g => new GroupWorkoutOutput(g)).ToList();
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