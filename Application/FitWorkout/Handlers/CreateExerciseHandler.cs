using Application.FitWorkout.Commands.CreateExercise;
using Application.FitWorkout.UseCase;
using Application.S3.UseCase;
using Domain.Base.Communication;
using Domain.Base.Messages.CommonMessages.Notification;
using Domain.DTOs.FitWorkout;
using MediatR;

namespace Application.FitWorkout.Handlers
{
    public class CreateExerciseHandler : IRequestHandler<CreateExerciseCommand, bool>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IFitWorkoutUseCase _fitWorkoutUseCase;
        private readonly IS3UseCase _s3UseCase;

        public CreateExerciseHandler(IMediatorHandler mediatorHandler,
            IFitWorkoutUseCase fitWorkoutUseCase,
            IS3UseCase s3UseCase)
        {
            _mediatorHandler = mediatorHandler;
            _fitWorkoutUseCase = fitWorkoutUseCase;
            _s3UseCase = s3UseCase;
        }

        public async Task<bool> Handle(CreateExerciseCommand request, CancellationToken cancellationToken)
        {
            if (request.IsValid())
            {
                var upload = await _s3UseCase.UploadFile(request.Upload);
                if (!string.IsNullOrEmpty(upload.ImgPath))
                {
                    var workout = new FitWorkoutDto(upload, request.Upload.GymId, 0);
                    await _fitWorkoutUseCase.AddWorkout(workout);
                    return true;
                }

                await _mediatorHandler.PublishNotification(new DomainNotification(request.MessageType, "Ocorreu um erro ao criar o exercício"));
            }
            foreach (var error in request.ValidationResult.Errors)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification(request.MessageType, error.ErrorMessage));
            }
            return true;
        }
    }
}
