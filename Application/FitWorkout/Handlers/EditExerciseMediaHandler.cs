using Application.FitWorkout.Commands.CreateExercise;
using Application.FitWorkout.Commands.EditExerciseData;
using Application.FitWorkout.Commands.EditExerciseMedia;
using Application.FitWorkout.UseCase;
using Application.S3.Boundaries;
using Application.S3.UseCase;
using Domain.Base.Communication;
using Domain.Base.Messages.CommonMessages.Notification;
using Domain.DTOs.FitWorkout;
using MediatR;

namespace Application.FitWorkout.Handlers
{
    public class EditExerciseMediaHandler : IRequestHandler<EditExerciseMediaCommand, bool>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IFitWorkoutUseCase _fitWorkoutUseCase;
        private readonly IS3UseCase _s3UseCase;

        public EditExerciseMediaHandler(IMediatorHandler mediatorHandler,
            IFitWorkoutUseCase fitWorkoutUseCase,
            IS3UseCase s3UseCase)
        {
            _mediatorHandler = mediatorHandler;
            _fitWorkoutUseCase = fitWorkoutUseCase;
            _s3UseCase = s3UseCase;
        }

        public async Task<bool> Handle(EditExerciseMediaCommand request, CancellationToken cancellationToken)
        {
            var input = request.Input;
            if (request.IsValid())
            {
                var exercise = await _fitWorkoutUseCase.GetByIdWithoutS3Url(input.WorkoutId);

                if (exercise != null)
                {
                    var uploadInput = new UploadInput(exercise.WorkoutName, input.Video!, input.Img!, exercise.GymId);

                    var upload = await _s3UseCase.UploadFile(uploadInput);

                    if (!string.IsNullOrEmpty(upload.ImgPath))
                    {
                        var workout = new FitWorkoutDto(upload, exercise.GymId);
                        await _fitWorkoutUseCase.UpdateWorkoutData(workout);

                        if (upload.ImgPath != exercise.ImgPath)
                        {
                            await _s3UseCase.DeleteObject(exercise.ImgPath);
                        }
                        if (upload.VideoPath != exercise.S3Path)
                        {
                            await _s3UseCase.DeleteObject(exercise.S3Path);
                        }
                        return true;
                    }
                    await _mediatorHandler.PublishNotification(new DomainNotification(request.MessageType, "Ocorreu um erro ao fazer o upload do arquivo"));
                    return true;
                }

                await _mediatorHandler.PublishNotification(new DomainNotification(request.MessageType, "Exercício não encontrado"));
            }
            foreach (var error in request.ValidationResult.Errors)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification(request.MessageType, error.ErrorMessage));
            }
            return true;
        }
    }
}
