using Application.User.Commands.SignIn;
using Application.User.UseCase;
using Domain.Base.Communication;
using Domain.Base.Messages.CommonMessages.Notification;
using Domain.DTOs.Token;
using MediatR;

namespace Application.User.Handlers
{
    public class SignInHandler : IRequestHandler<SignInCommand, TokenDto>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IUserUseCase _userUseCase;

        public SignInHandler(IUserUseCase userUseCase, IMediatorHandler mediatorHandler)
        {
            _userUseCase = userUseCase;
            _mediatorHandler = mediatorHandler;
        }
        public async Task<TokenDto> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            if(request.IsValid())
            {
                var result = await _userUseCase.SignIn(request.Input);

                if(string.IsNullOrEmpty(result.UserToken))
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
