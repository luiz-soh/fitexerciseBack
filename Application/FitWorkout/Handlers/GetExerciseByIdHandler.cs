
using Application.FitWorkout.Boundaries;
using Application.FitWorkout.Commands.GetExerciseById;
using Application.FitWorkout.UseCase;
using Domain.Base.Communication;
using Domain.Base.Messages.CommonMessages.Notification;
using MediatR;

namespace Application.FitWorkout.Handlers
{
    public class GetExerciseByIdHandler : IRequestHandler<GetExerciseByIdCommand, FullExerciseOutput>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IFitWorkoutUseCase _fitWorkoutUseCase;

        public GetExerciseByIdHandler(IMediatorHandler mediatorHandler,
            IFitWorkoutUseCase fitWorkoutUseCase)
        {
            _mediatorHandler = mediatorHandler;
            _fitWorkoutUseCase = fitWorkoutUseCase;
        }

        public async Task<FullExerciseOutput> Handle(GetExerciseByIdCommand request, CancellationToken cancellationToken)
        {
            if (request.IsValid())
            {
                var exercise = await _fitWorkoutUseCase.GetExerciseById(request.Id);
                if (exercise != null)
                {
                    return new FullExerciseOutput(exercise);
                }
            }
            foreach (var error in request.ValidationResult.Errors)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification(request.MessageType, error.ErrorMessage));
            }
            return new FullExerciseOutput();
        }
    }
}
