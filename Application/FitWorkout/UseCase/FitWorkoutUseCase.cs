using Domain.Base.Paginated;
using Domain.DTOs.FitWorkout;
using Domain.Entities.FitWorkout;

namespace Application.FitWorkout.UseCase
{
    public class FitWorkoutUseCase : IFitWorkoutUseCase
    {
        private readonly IFitWorkoutRepository _fitWorkoutRepository;

        public FitWorkoutUseCase(IFitWorkoutRepository fitWorkoutRepository)
        {
            _fitWorkoutRepository = fitWorkoutRepository;
        }

        public async Task AddWorkout(FitWorkoutDto fitWorkout)
        {
            await _fitWorkoutRepository.AddWorkout(fitWorkout);
        }

        public async Task<List<GetExercisesDto>> GetExercises(int userId)
        {
            return await _fitWorkoutRepository.GetExercises(userId);
        }

        public async Task<GetExerciseByIdDto> GetExerciseById(int id)
        {
            return await _fitWorkoutRepository.GetById(id);
        }

        public async Task<PaginatedDto<GetExercisesDto>> GetAllExercisesByGymId(int gymId, int? perPage, int? page, string orderBy, string order, string? search)
        {
            return  await _fitWorkoutRepository.GetAllExercisesByGymId(gymId, perPage, page, orderBy, order, search);
        }

        public async Task<List<GetExercisesDto>> GetExercisesByGymId(int userId, int gymId)
        {
            return await _fitWorkoutRepository.GetExercisesByGymId(userId, gymId);
        }

        public async Task<GetExerciseByIdDto> GetByIdWithoutS3Url(int id)
        {
            return await _fitWorkoutRepository.GetByIdWithoutS3Url(id);
        }

        public async Task UpdateWorkoutData(FitWorkoutDto fitWorkout)
        {
            await _fitWorkoutRepository.UpdateWorkout(fitWorkout);
        }
    }
}