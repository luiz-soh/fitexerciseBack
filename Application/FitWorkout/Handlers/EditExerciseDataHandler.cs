using Application.FitWorkout.Commands.CreateExercise;
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
                    var imgNewPath = _s3UseCase.EditFileLocation(exercise.ImgPath, input.ExerciseName);
                    var videoNewPath = _s3UseCase.EditFileLocation(exercise.S3Path, input.ExerciseName);

                    var imageResponse = await _s3UseCase.CopyObject(exercise.ImgPath, imgNewPath);
                    var videoResponse = await _s3UseCase.CopyObject(exercise.S3Path, videoNewPath);

                    if (!imageResponse || !videoResponse)
                    {
                        await _mediatorHandler.PublishNotification(new DomainNotification(request.MessageType, "ocorreu um erro ao atualizar o exercício"));
                        return false;
                    }

                    exercise.WorkoutName = input.ExerciseName;

                    var exerciseDto = new FitWorkoutDto(exercise, videoNewPath, imgNewPath);
                    await _fitWorkoutUseCase.UpdateWorkoutData(exerciseDto);

                    await _s3UseCase.DeleteObject(exercise.S3Path);
                    await _s3UseCase.DeleteObject(exercise.ImgPath);
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
