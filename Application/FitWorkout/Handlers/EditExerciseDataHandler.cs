using Application.FitWorkout.Commands.EditExerciseData;
using Application.FitWorkout.UseCase;
using Application.S3.UseCase;
using Domain.Base.Communication;
using Domain.Base.Messages.CommonMessages.Notification;
using Domain.DTOs.FitWorkout;
using MediatR;

namespace Application.FitWorkout.Handlers
{
    public class EditExerciseDataHandler : IRequestHandler<EditExerciseDataCommand, bool>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IFitWorkoutUseCase _fitWorkoutUseCase;
        private readonly IS3UseCase _s3UseCase;

        public EditExerciseDataHandler(IMediatorHandler mediatorHandler,
            IFitWorkoutUseCase fitWorkoutUseCase,
            IS3UseCase s3UseCase)
        {
            _mediatorHandler = mediatorHandler;
            _fitWorkoutUseCase = fitWorkoutUseCase;
            _s3UseCase = s3UseCase;
        }

        public async Task<bool> Handle(EditExerciseDataCommand request, CancellationToken cancellationToken)
        {
            var input = request.Input;
            if (request.IsValid())
            {
                var exercise = await _fitWorkoutUseCase.GetByIdWithoutS3Url(input.WorkoutId);

                if (exercise != null)
                {
                    var changedExerciseName = input.ExerciseName != exercise.WorkoutName;
                    var imgPath = exercise.ImgPath;
                    var videoPath = exercise.S3Path;

                    if (changedExerciseName)
                    {
                        imgPath = _s3UseCase.EditFileLocation(exercise.ImgPath, input.ExerciseName);
                        videoPath = _s3UseCase.EditFileLocation(exercise.S3Path, input.ExerciseName);

                        var imageResponse = await _s3UseCase.CopyObject(exercise.ImgPath, imgPath);
                        var videoResponse = await _s3UseCase.CopyObject(exercise.S3Path, videoPath);

                        if (!imageResponse || !videoResponse)
                        {
                            await _mediatorHandler.PublishNotification(new DomainNotification(request.MessageType, "ocorreu um erro ao atualizar o exercício"));
                            return false;
                        }
                    }

                    exercise.WorkoutName = input.ExerciseName;
                    exercise.Type = input.Type;

                    var exerciseDto = new FitWorkoutDto(exercise, videoPath, imgPath);
                    await _fitWorkoutUseCase.UpdateWorkoutData(exerciseDto);

                    if (changedExerciseName)
                    {
                        await _s3UseCase.DeleteObject(exercise.S3Path);
                        await _s3UseCase.DeleteObject(exercise.ImgPath);
                    }
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
