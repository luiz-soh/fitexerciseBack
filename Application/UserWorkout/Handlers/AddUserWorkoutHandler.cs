using Application.UserWorkout.Commands;
using Application.UserWorkout.UseCase;
using Domain.Base.Communication;
using Domain.Base.Messages.CommonMessages.Notification;
using Domain.DTOs.UserWorkout;
using MediatR;

namespace Application.UserWorkout.Handlers
{
    public class AddUserWorkoutHandlerOld : IRequestHandler<AddUserWorkoutCommandOld, bool>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IUserWorkoutUseCase _useCase;

        public AddUserWorkoutHandlerOld(IUserWorkoutUseCase useCase, IMediatorHandler handler)
        {
            _useCase = useCase;
            _mediatorHandler = handler;
        }

        public async Task<bool> Handle(AddUserWorkoutCommandOld request, CancellationToken cancellationToken)
        {
            if (request.IsValid())
            {
                try
                {
                    var input = request.Input;
                    var dto = new AddUserWorkoutDto(input.UserId, input.WorkoutId, input.GroupWorkoutId, input.WorkoutSeries, input.WorkoutRepetition);
                    await _useCase.AddUserWorkout(dto);
                    return true;
                }
                catch
                {
                    await _mediatorHandler.PublishNotification(new DomainNotification(request.MessageType, "Ocorreu um erro ao tentar adicionar exercicio"));
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