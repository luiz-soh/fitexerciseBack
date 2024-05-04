using Domain.DTOs.FitWorkout;

namespace Application.FitWorkout.UseCase
{
    public interface IFitWorkoutUseCase
    {
        Task AddWorkout(FitWorkoutDto fitWorkout);
        Task<List<GetExercisesDto>> GetExercises(int userId);
        Task<GetExerciseByIdDto> GetExerciseById(int id);
        Task<GetExerciseByIdDto> GetByIdWithoutS3Url(int id);
        Task<List<GetExercisesDto>> GetAllExercisesByGymId(int gymId);
        Task<List<GetExercisesDto>> GetExercisesByGymId(int userId, int gymId);
        Task UpdateWorkoutData(FitWorkoutDto fitWorkout);
    }
}
