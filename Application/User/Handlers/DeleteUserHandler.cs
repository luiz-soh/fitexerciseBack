using Application.User.Commands.DeleteUser;
using Application.User.UseCase;
using Domain.Base.Communication;
using Domain.Base.Messages.CommonMessages.Notification;
using MediatR;

namespace Application.User.Handlers
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IUserUseCase _userUseCase;

        public DeleteUserHandler(IMediatorHandler mediatorHandler, IUserUseCase userUseCase)
        {
            _mediatorHandler = mediatorHandler;
            _userUseCase = userUseCase;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            if (request.IsValid())
            {
                return await _userUseCase.DeleteUser(request.Id);
            }

            foreach (var error in request.ValidationResult.Errors)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification(request.MessageType, error.ErrorMessage));
            }

            return false;
        }
    }
}