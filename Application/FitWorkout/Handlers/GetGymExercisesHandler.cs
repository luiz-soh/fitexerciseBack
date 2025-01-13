using Application.FitWorkout.Boundaries;
using Application.FitWorkout.Commands.GetGymExercises;
using Application.FitWorkout.UseCase;
using Domain.Base.Communication;
using Domain.Base.Messages.CommonMessages.Notification;
using MediatR;

namespace Application.FitWorkout.Handlers
{
    public class GetGymExercisesHandler : IRequestHandler<GetGymExecisesCommand, PaginatedExercisesOutput>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IFitWorkoutUseCase _fitWorkoutUseCase;

        public GetGymExercisesHandler(IMediatorHandler mediatorHandler,
            IFitWorkoutUseCase fitWorkoutUseCase)
        {
            _mediatorHandler = mediatorHandler;
            _fitWorkoutUseCase = fitWorkoutUseCase;
        }

        public async Task<PaginatedExercisesOutput> Handle(GetGymExecisesCommand request, CancellationToken cancellationToken)
        {
            var input = request.Input;
            if (request.IsValid())
            {
                var exercises = await _fitWorkoutUseCase.GetAllExercisesByGymId(input.GymId, input.PerPage, input.Page,
                 input.Orderby ?? string.Empty, input.Order ?? string.Empty, input.Search);
                if (exercises != null)
                {
                    return new PaginatedExercisesOutput(exercises);
                }
            }
            foreach (var error in request.ValidationResult.Errors)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification(request.MessageType, error.ErrorMessage));
            }
            return new PaginatedExercisesOutput();
        }
    }
}
