using Domain.Base.Paginated;
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
        Task<PaginatedDto<GetExercisesDto>> GetAllExercisesByGymId(int gymId, int? perPage, int? page,
            string orderBy, string order, string? search);
        Task<List<GetExercisesDto>> GetExercisesByGymId(int userId, int gymId);
    }
}
