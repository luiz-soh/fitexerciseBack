using Application.Gym.Commands.CreateGym;
using Application.Gym.UseCase;
using Domain.Base.Communication;
using Domain.Base.Messages.CommonMessages.Notification;
using MediatR;

namespace Application.Gym.Handlers
{
    public class CreateGymHandler : IRequestHandler<CreateGymCommand, bool>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IGymUseCase _gymUseCase;

        public CreateGymHandler(IGymUseCase gymUseCase, IMediatorHandler mediatorHandler)
        {
            _gymUseCase = gymUseCase;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<bool> Handle(CreateGymCommand request, CancellationToken cancellationToken)
        {
            if (request.IsValid())
            {
                var input = request.Input;
                var exists = await _gymUseCase.GymExists(input.Login);

                if (exists)
                {
                    await _mediatorHandler.PublishNotification(new DomainNotification(request.MessageType, "Usuario ja está sendo utilizado"));
                }
                else
                {
                    await _gymUseCase.CreateGym(input);

                    return true;
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