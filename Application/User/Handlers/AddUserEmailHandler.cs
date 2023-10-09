using Application.User.Commands.AddUserEmail;
using Application.User.UseCase;
using Domain.Base.Communication;
using Domain.Base.Messages.CommonMessages.Notification;
using MediatR;

namespace Application.User.Handlers
{
    public class AddUserEmailHandler : IRequestHandler<AddUserEmailCommand, bool>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IUserUseCase _userUseCase;

        public AddUserEmailHandler(IMediatorHandler mediatorHandler,
            IUserUseCase userUseCase)
        {
            _mediatorHandler = mediatorHandler;
            _userUseCase = userUseCase;
        }

        public async Task<bool> Handle(AddUserEmailCommand request, CancellationToken cancellationToken)
        {
            if (request.IsValid())
            {
                await _userUseCase.AddUserEmail(request.Input);

                return true;
            }

            foreach (var error in request.ValidationResult.Errors)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification(request.MessageType, error.ErrorMessage));
            }

            return false;
        }
    }
}
