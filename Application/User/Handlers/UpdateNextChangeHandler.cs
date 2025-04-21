using Application.User.Commands.UpdateNextChange;
using Application.User.UseCase;
using Domain.Base.Communication;
using Domain.Base.Messages.CommonMessages.Notification;
using MediatR;

namespace Application.User.Handlers
{
    public class UpdateNextChangeHandler(IMediatorHandler mediatorHandler, IUserUseCase userUseCase) : IRequestHandler<UpdateNextChangeCommand, bool>
    {
        private readonly IMediatorHandler _mediatorHandler = mediatorHandler;
        private readonly IUserUseCase _userUseCase = userUseCase;

        public async Task<bool> Handle(UpdateNextChangeCommand request, CancellationToken cancellationToken)
        {
            if (request.IsValid())
            {
                var input = request.Input;
                var user = await _userUseCase.GetUserData(request.UserId);

                if (user.Id == 0)
                {
                    await _mediatorHandler.PublishNotification(new DomainNotification(request.MessageType, "Usuário não encontrado"));
                }
                else
                {
                    await _userUseCase.UpdateNextChange(request.UserId, input.NextChange);

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