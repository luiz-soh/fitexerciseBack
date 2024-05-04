using Domain.DTOs.FitWorkout;

namespace Domain.Entities.FitWorkout
{
    public interface IFitWorkoutRepository
    {
        Task AddWorkout(FitWorkoutDto input);
        Task UpdateWorkout(FitWorkoutDto input);
        Task<List<GetExercisesDto>> GetExercises(int userId);
        Task<GetExerciseByIdDto> GetById(int id);
        Task<GetExerciseByIdDto> GetByIdWithoutS3Url(int id);
        Task<List<GetExercisesDto>> GetAllExercisesByGymId(int gymId);
        Task<List<GetExercisesDto>> GetExercisesByGymId(int userId, int gymId);
    }
}
