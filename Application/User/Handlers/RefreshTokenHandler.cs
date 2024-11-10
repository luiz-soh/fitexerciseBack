using Application.User.Commands.RefreshToken;
using Application.User.Commands.SignIn;
using Application.User.UseCase;
using Domain.Base.Communication;
using Domain.Base.Messages.CommonMessages.Notification;
using Domain.DTOs.Token;
using MediatR;

namespace Application.User.Handlers
{
    public class RefreshTokenHandler(IUserUseCase userUseCase, IMediatorHandler mediatorHandler) : IRequestHandler<RefreshTokenCommand, TokenDto>
    {
        private readonly IMediatorHandler _mediatorHandler = mediatorHandler;
        private readonly IUserUseCase _userUseCase = userUseCase;

        public async Task<TokenDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            if (request.IsValid())
            {
                var result = await _userUseCase.UpdateToken(request.Input);

                if (string.IsNullOrEmpty(result.UserToken))
                {
                    await _mediatorHandler.PublishNotification(new DomainNotification(request.MessageType, "Usuário ou senha inválidos"));
                }

                return result;
            }

            foreach (var error in request.ValidationResult.Errors)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification(request.MessageType, error.ErrorMessage));
            }

            return new TokenDto();
        }
    }
}
