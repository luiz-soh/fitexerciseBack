using Application.UserWorkout.Commands;
using Application.UserWorkout.UseCase;
using Domain.Base.Communication;
using Domain.Base.Messages.CommonMessages.Notification;
using Domain.DTOs.UserWorkout;
using MediatR;

namespace Application.UserWorkout.Handlers
{
    public class DeleteUserWorkoutHandler : IRequestHandler<DeleteUserWorkoutCommand, bool>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IUserWorkoutUseCase _useCase;

        public DeleteUserWorkoutHandler(IUserWorkoutUseCase useCase, IMediatorHandler handler)
        {
            _useCase = useCase;
            _mediatorHandler = handler;
        }

        public async Task<bool> Handle(DeleteUserWorkoutCommand request, CancellationToken cancellationToken)
        {
            if (request.IsValid())
            {
                try
                {

                    await _useCase.DeleteUserWorkoutId(request.UserId, request.WorkoutId);
                    return true;
                }
                catch
                {
                    await _mediatorHandler.PublishNotification(new DomainNotification(request.MessageType, "Ocorreu um erro ao tentar excluir exercicio"));
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