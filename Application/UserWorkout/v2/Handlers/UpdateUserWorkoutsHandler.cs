using Application.UserWorkout.Commands;
using Application.UserWorkout.UseCase;
using Application.UserWorkout.v2.Commands;
using Domain.Base.Communication;
using Domain.Base.Messages.CommonMessages.Notification;
using Domain.DTOs.UserWorkout;
using MediatR;

namespace Application.UserWorkout.v2.Handlers
{
    public class UpdateUserWorkoutsHandler : IRequestHandler<UpdateUserWorkoutsCommand, bool>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IUserWorkoutUseCase _useCase;

        public UpdateUserWorkoutsHandler(IUserWorkoutUseCase useCase, IMediatorHandler handler)
        {
            _useCase = useCase;
            _mediatorHandler = handler;
        }
        public async Task<bool> Handle(UpdateUserWorkoutsCommand request, CancellationToken cancellationToken)
        {
            if (request.IsValid())
            {
                try
                {
                    var dto = request.Input.Workouts!.Select(x => new DynamoUserWorkoutDto(x.Position, x.Series, x.Repetitions, x.WorkoutId, x.WorkoutName)).ToList();
                    await _useCase.UpdateUserWorkouts(request.Input.UserId, request.Input.GroupId, dto);
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