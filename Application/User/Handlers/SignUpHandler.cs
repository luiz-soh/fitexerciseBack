using Application.User.Commands.SignUp;
using Application.User.UseCase;
using Domain.Base.Communication;
using Domain.Base.Messages.CommonMessages.Notification;
using Domain.DTOs.Authentication;
using MediatR;

namespace Application.User.Handlers
{
    public class SignUpHandler : IRequestHandler<SignUpCommand, bool>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IUserUseCase _userUseCase;

        public SignUpHandler(IMediatorHandler mediatorHandler, IUserUseCase userUseCase)
        {
            _mediatorHandler = mediatorHandler;
            _userUseCase = userUseCase;
        }

        public async Task<bool> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            if (request.IsValid())
            {
                var input = request.Input;
                var userExists = await _userUseCase.UserExists(input.Username, input.UserEmail ?? string.Empty);

                if (userExists)
                {
                    await _mediatorHandler.PublishNotification(new DomainNotification(request.MessageType, "Usuário ou e-mail ja cadastrado"));
                }
                else
                {
                    await _userUseCase.SignUp(input);

                    return true;
                }
            }

            foreach (var error in request.ValidationResult.Errors)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification(request.MessageType, error.ErrorMessage));
            }

            return false;
        }
    }
}