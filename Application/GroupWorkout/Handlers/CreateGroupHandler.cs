using Application.GroupWorkout.Commands.CreateGroup;
using Application.GroupWorkout.UseCase;
using Domain.Base.Communication;
using Domain.Base.Messages.CommonMessages.Notification;
using MediatR;

namespace Application.GroupWorkout.Handlers
{
    public class CreateGroupHandler : IRequestHandler<CreateGroupCommand, bool>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IGroupWorkoutUseCase _useCase;

        public CreateGroupHandler(IGroupWorkoutUseCase useCase, IMediatorHandler handler)
        {
            _useCase = useCase;
            _mediatorHandler = handler;
        }

        public async Task<bool> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            if (request.IsValid())
            {
                try
                {
                    return await _useCase.AddGroupWorkout(request.UserId, request.Input.Name);
                }
                catch
                {
                    await _mediatorHandler.PublishNotification(new DomainNotification(request.MessageType, "Ocorreu um erro ao tentar adicionar um novo grupo"));
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