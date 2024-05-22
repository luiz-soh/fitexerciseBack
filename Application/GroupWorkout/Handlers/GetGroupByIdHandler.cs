using Application.GroupWorkout.Boundaries;
using Application.GroupWorkout.Commands.GetGroupById;
using Application.GroupWorkout.UseCase;
using Domain.Base.Communication;
using Domain.Base.Messages.CommonMessages.Notification;
using MediatR;

namespace Application.GroupWorkout.Handlers
{
    public class GetGroupByIdHandler : IRequestHandler<GetGroupByIdCommand, GroupWorkoutOutput>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IGroupWorkoutUseCase _useCase;

        public GetGroupByIdHandler(IGroupWorkoutUseCase useCase, IMediatorHandler handler)
        {
            _useCase = useCase;
            _mediatorHandler = handler;
        }

        public async Task<GroupWorkoutOutput> Handle(GetGroupByIdCommand request, CancellationToken cancellationToken)
        {
            if (request.IsValid())
            {
                var group = await _useCase.GetGroupById(request.Id);

                return new GroupWorkoutOutput(group);
            }
            else
            {
                foreach (var error in request.ValidationResult.Errors)
                {
                    await _mediatorHandler.PublishNotification(new DomainNotification(request.MessageType, error.ErrorMessage));
                }
            }

            return new GroupWorkoutOutput();
        }
    }
}