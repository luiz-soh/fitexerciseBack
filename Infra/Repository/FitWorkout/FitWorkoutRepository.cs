using Domain.Configuration;
using Domain.DTOs.FitWorkout;
using Domain.Entities.FitWorkout;
using Infra.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infra.Repository.FitWorkout
{
    public class FitWorkoutRepository : IFitWorkoutRepository, IDisposable
    {
        private readonly DbContextOptions<ContextBase> _optionsBuilder;
        private readonly IOptions<Secrets> _secrets;
        private readonly ContextBase _contextBase;

        public FitWorkoutRepository(IOptions<Secrets> secrets)
        {
            _optionsBuilder = new DbContextOptions<ContextBase>();
            _secrets = secrets;
            _contextBase = new ContextBase(_optionsBuilder, _secrets);
        }

        public async Task AddWorkout(FitWorkoutDto input)
        {
            var workout = new FitWorkoutEntity(input);
            await _contextBase.FitWorkout.AddAsync(workout);
            await _contextBase.SaveChangesAsync();
        }

        public async Task<GetExerciseByIdDto> GetById(int id)
        {
            var workout = await _contextBase.FitWorkout.Where(x => x.WorkoutId == id)
                .Select(x =>
                new GetExerciseByIdDto(
                            x,
                           _secrets.Value.S3Url + x.S3Path,
                           _secrets.Value.S3Url + x.ImgPath
                    )).AsNoTracking().FirstOrDefaultAsync();

            if (workout == null)
                return new GetExerciseByIdDto();
            else
                return workout;
        }

        public async Task<GetExerciseByIdDto> GetByIdWithoutS3Url(int id)
        {
            var workout = await _contextBase.FitWorkout.Where(x => x.WorkoutId == id)
                .Select(x =>
                new GetExerciseByIdDto(x)).AsNoTracking().FirstOrDefaultAsync();

            if (workout == null)
                return new GetExerciseByIdDto();
            else
                return workout;
        }

        public async Task UpdateWorkout(FitWorkoutDto input)
        {
            var workout = new FitWorkoutEntity(input);
            _contextBase.FitWorkout.Update(workout);
            await _contextBase.SaveChangesAsync();
        }

        public async Task<List<GetExercisesDto>> GetExercises(int userId)
        {
            return await (from workout in _contextBase.FitWorkout
                          where !_contextBase.UserWorkout.Any(x => x.UserId == userId && x.WorkoutId == workout.WorkoutId)
                          orderby workout.WorkoutName
                          select new
                          GetExercisesDto(
                           workout.WorkoutId,
                           workout.WorkoutName,
                           _secrets.Value.S3Url + workout.S3Path,
                           _secrets.Value.S3Url + workout.ImgPath,
                           workout.Type
                          )).AsNoTracking().ToListAsync();
        }

        public async Task<List<GetExercisesDto>> GetAllExercisesByGymId(int gymId)
        {
            return await _contextBase.FitWorkout.Where(x => x.GymId == gymId || (gymId == 1 && x.GymId == null)).Select(x => new GetExercisesDto(
                           x.WorkoutId,
                           x.WorkoutName,
                           _secrets.Value.S3Url + x.S3Path,
                           _secrets.Value.S3Url + x.ImgPath,
                           x.Type
                          )).AsNoTracking().ToListAsync();
        }

        public async Task<List<GetExercisesDto>> GetExercisesByGymId(int userId, int gymId)
        {
            return await (from workout in _contextBase.FitWorkout
                          where !_contextBase.UserWorkout.Any(x => x.UserId == userId && x.WorkoutId == workout.WorkoutId) &&
                          (workout.GymId == gymId || (gymId == 1 && workout.GymId == null))
                          select new
                          GetExercisesDto(
                           workout.WorkoutId,
                           workout.WorkoutName,
                           _secrets.Value.S3Url + workout.S3Path,
                           _secrets.Value.S3Url + workout.ImgPath,
                           workout.Type
                          )).AsNoTracking().ToListAsync();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}