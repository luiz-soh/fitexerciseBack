using Application.UserWorkout.Commands;
using Application.UserWorkout.UseCase;
using Domain.Base.Communication;
using Domain.Base.Messages.CommonMessages.Notification;
using Domain.DTOs.UserWorkout;
using MediatR;

namespace Application.UserWorkout.Handlers
{
    public class UpdateUserWorkoutHandler : IRequestHandler<UpdateUserWorkoutCommand, bool>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IUserWorkoutUseCase _useCase;

        public UpdateUserWorkoutHandler(IUserWorkoutUseCase useCase, IMediatorHandler handler)
        {
            _useCase = useCase;
            _mediatorHandler = handler;
        }

        public async Task<bool> Handle(UpdateUserWorkoutCommand request, CancellationToken cancellationToken)
        {
            if (request.IsValid())
            {
                try
                {
                    var input = request.Input;
                    var dto = new UpdateUserWorkoutDto(input.UserId, input.WorkoutId, input.GroupWorkoutId,
                     input.WorkoutSeries, input.WorkoutRepetition, input.WorkoutPosition, input.UwId);
                    await _useCase.UpdateUserWorkout(dto);
                    return true;
                }
                catch
                {
                    await _mediatorHandler.PublishNotification(new DomainNotification(request.MessageType, "Ocorreu um erro ao tentar atualizar exercicio"));
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