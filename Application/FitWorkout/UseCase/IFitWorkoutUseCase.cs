using Domain.Base.Paginated;
using Domain.DTOs.FitWorkout;

namespace Application.FitWorkout.UseCase
{
    public interface IFitWorkoutUseCase
    {
        Task AddWorkout(FitWorkoutDto fitWorkout);
        Task<List<GetExercisesDto>> GetExercises(int userId);
        Task<GetExerciseByIdDto> GetExerciseById(int id);
        Task<GetExerciseByIdDto> GetByIdWithoutS3Url(int id);
        Task<PaginatedDto<GetExercisesDto>> GetAllExercisesByGymId(int gymId, int? perPage, int? page, string orderBy, string order, string? search);
        Task<List<GetExercisesDto>> GetExercisesByGymId(int userId, int gymId);
        Task UpdateWorkoutData(FitWorkoutDto fitWorkout);
    }
}
