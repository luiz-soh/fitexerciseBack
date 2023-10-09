using Application.User.Commands.GetUserData;
using Application.User.UseCase;
using Domain.Base.Communication;
using Domain.Base.Messages.CommonMessages.Notification;
using Domain.DTOs.User;
using MediatR;

namespace Application.User.Handlers
{
    public class GetUserDataHandler : IRequestHandler<GetUserDataCommand, UserDto>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IUserUseCase _userUseCase;

        public GetUserDataHandler(IUserUseCase userUseCase, IMediatorHandler mediatorHandler)
        {
            _userUseCase = userUseCase;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<UserDto> Handle(GetUserDataCommand request, CancellationToken cancellationToken)
        {
            if (request.IsValid())
            {
                return await _userUseCase.GetUserData(request.UserId);
            }

            foreach (var error in request.ValidationResult.Errors)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification(request.MessageType, error.ErrorMessage));
            }

            return new UserDto();
        }
    }
}
