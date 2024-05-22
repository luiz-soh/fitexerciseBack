using Application.GroupWorkout.Commands.DeleteGroupById;
using Application.GroupWorkout.UseCase;
using Domain.Base.Communication;
using Domain.Base.Messages.CommonMessages.Notification;
using MediatR;

namespace Application.GroupWorkout.Handlers
{
    public class DeleteGroupByIdHandler : IRequestHandler<DeleteGroupByIdCommand, bool>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IGroupWorkoutUseCase _useCase;

        public DeleteGroupByIdHandler(IGroupWorkoutUseCase useCase, IMediatorHandler handler)
        {
            _useCase = useCase;
            _mediatorHandler = handler;
        }

        public async Task<bool> Handle(DeleteGroupByIdCommand request, CancellationToken cancellationToken)
        {
            if (request.IsValid())
            {
                try
                {
                    return await _useCase.DeleteGroupWorkout(request.Id, request.UserId);
                }
                catch
                {
                    await _mediatorHandler.PublishNotification(new DomainNotification(request.MessageType, "Ocorreu um erro ao tentar deletar grupo"));
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