using Application.UserWorkout.Boundaries;
using Application.UserWorkout.Commands;
using Application.UserWorkout.UseCase;
using Domain.Base.Communication;
using Domain.Base.Messages.CommonMessages.Notification;
using MediatR;

namespace Application.UserWorkout.Handlers
{
    public class GetUserExercisesHandler : IRequestHandler<GetUserExercisesCommand, List<UserExerciseOutput>>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IUserWorkoutUseCase _useCase;

        public GetUserExercisesHandler(IUserWorkoutUseCase useCase, IMediatorHandler handler)
        {
            _useCase = useCase;
            _mediatorHandler = handler;
        }

        public async Task<List<UserExerciseOutput>> Handle(GetUserExercisesCommand request, CancellationToken cancellationToken)
        {
            if (request.IsValid())
            {
                var dto = await _useCase.GetUserExercises(request.UserId, request.GroupId);

                return dto.Select(x => new UserExerciseOutput(x)).ToList();
            }
            else
            {
                foreach (var error in request.ValidationResult.Errors)
                {
                    await _mediatorHandler.PublishNotification(new DomainNotification(request.MessageType, error.ErrorMessage));
                }
            }

            return [];
        }
    }
}