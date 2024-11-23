using Application.User.Commands.RecoverPassword;
using Application.User.UseCase;
using Domain.Base.Communication;
using Domain.Base.Messages.CommonMessages.Notification;
using MediatR;

namespace Application.User.Handlers
{
    public class RecoverPasswordHandler(IMediatorHandler mediatorHandler,
        IUserUseCase userUseCase) : IRequestHandler<RecoverPasswordCommand, bool>
    {
        private readonly IMediatorHandler _mediatorHandler = mediatorHandler;
        private readonly IUserUseCase _userUseCase = userUseCase;

        public async Task<bool> Handle(RecoverPasswordCommand request, CancellationToken cancellationToken)
        {
            if (request.IsValid())
            {
                var userId = await _userUseCase.GetUserIdByRecoveryCode(request.Input.RecoverCode);

                if (userId > 0)
                {
                    var sucess = await _userUseCase.RecoverUserPassword(userId, request.Input.Password);

                    if (sucess)
                        return sucess;
                    else
                        await _mediatorHandler.PublishNotification(new DomainNotification(request.MessageType, "ocorreu um erro ao alterar a senha"));
                }
                else
                {
                    await _mediatorHandler.PublishNotification(new DomainNotification(request.MessageType, "Código inválido"));
                }

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
