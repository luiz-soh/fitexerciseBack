using Application.Gym.Commands.CreateGym;
using Application.Gym.Commands.LogIn;
using Application.Gym.UseCase;
using Domain.Base.Communication;
using Domain.Base.Messages.CommonMessages.Notification;
using Domain.DTOs.Gym;
using MediatR;

namespace Application.Gym.Handlers
{
    public class LogInHandler : IRequestHandler<LogInCommand, GymTokenDto>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IGymUseCase _gymUseCase;

        public LogInHandler(IGymUseCase gymUseCase, IMediatorHandler mediatorHandler)
        {
            _gymUseCase = gymUseCase;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<GymTokenDto> Handle(LogInCommand request, CancellationToken cancellationToken)
        {
            if (request.IsValid())
            {
                var result = await _gymUseCase.LogIn(request.Input);

                if (string.IsNullOrEmpty(result.Token))
                {
                    await _mediatorHandler.PublishNotification(new DomainNotification(request.MessageType, "Usuário ou senha inválidos"));
                }

                return result;
            }
            else
            {
                foreach (var error in request.ValidationResult.Errors)
                {
                    await _mediatorHandler.PublishNotification(new DomainNotification(request.MessageType, error.ErrorMessage));
                }
            }

            return new GymTokenDto();;
        }
    }
}