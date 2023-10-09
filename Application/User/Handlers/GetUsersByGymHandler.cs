using Application.User.Commands.GetUsersByGym;
using Application.User.UseCase;
using Domain.Base.Communication;
using Domain.Base.Messages.CommonMessages.Notification;
using Domain.DTOs.User;
using MediatR;

namespace Application.User.Handlers
{
    public class GetUsersByGymHandler : IRequestHandler<GetUsersByGymCommand, List<UserDto>>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IUserUseCase _userUseCase;

        public GetUsersByGymHandler(IMediatorHandler mediatorHandler, IUserUseCase userUseCase)
        {
            _mediatorHandler = mediatorHandler;
            _userUseCase = userUseCase;
        }

        public async Task<List<UserDto>> Handle(GetUsersByGymCommand request, CancellationToken cancellationToken)
        {
            if (request.IsValid())
            {
                return await _userUseCase.GetUsersByGymId(request.GymId);
            }

            foreach (var error in request.ValidationResult.Errors)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification(request.MessageType, error.ErrorMessage));
            }

            return new List<UserDto>();
        }
    }
}
