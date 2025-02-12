using Application.User.Boundaries.Output;
using Application.User.Commands.GetUsersByGym;
using Application.User.UseCase;
using Domain.Base.Communication;
using Domain.Base.Messages.CommonMessages.Notification;
using MediatR;

namespace Application.User.Handlers
{
    public class GetUsersByGymHandler : IRequestHandler<GetUsersByGymCommand, PaginatedUsersOutput>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IUserUseCase _userUseCase;

        public GetUsersByGymHandler(IMediatorHandler mediatorHandler, IUserUseCase userUseCase)
        {
            _mediatorHandler = mediatorHandler;
            _userUseCase = userUseCase;
        }

        public async Task<PaginatedUsersOutput> Handle(GetUsersByGymCommand request, CancellationToken cancellationToken)
        {
            var input = request.Input;
            if (request.IsValid())
            {
                var users = await _userUseCase.GetUsersByGymId(input.GymId, input.PerPage, input.Page,
                 input.Orderby ?? string.Empty, input.Order ?? string.Empty, input.Search);
                if (users != null)
                {
                    return new PaginatedUsersOutput(users);
                }
            }
            foreach (var error in request.ValidationResult.Errors)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification(request.MessageType, error.ErrorMessage));
            }
            return new PaginatedUsersOutput();
        }
    }
}
