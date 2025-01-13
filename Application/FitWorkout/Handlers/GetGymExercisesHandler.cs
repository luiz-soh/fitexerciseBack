using Application.FitWorkout.Boundaries;
using Application.FitWorkout.Commands.GetGymExercises;
using Application.FitWorkout.UseCase;
using Domain.Base.Communication;
using Domain.Base.Messages.CommonMessages.Notification;
using MediatR;

namespace Application.FitWorkout.Handlers
{
    public class GetGymExercisesHandler : IRequestHandler<GetGymExecisesCommand, List<ExerciseOutput>>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IFitWorkoutUseCase _fitWorkoutUseCase;

        public GetGymExercisesHandler(IMediatorHandler mediatorHandler,
            IFitWorkoutUseCase fitWorkoutUseCase)
        {
            _mediatorHandler = mediatorHandler;
            _fitWorkoutUseCase = fitWorkoutUseCase;
        }

        public async Task<List<ExerciseOutput>> Handle(GetGymExecisesCommand request, CancellationToken cancellationToken)
        {
            if (request.IsValid())
            {
                var exercises = await _fitWorkoutUseCase.GetAllExercisesByGymId(request.Id);
                if (exercises != null)
                {
                    return exercises.Select(x => new ExerciseOutput(x)).ToList();
                }
            }
            foreach (var error in request.ValidationResult.Errors)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification(request.MessageType, error.ErrorMessage));
            }
            return [];
        }
    }
}
