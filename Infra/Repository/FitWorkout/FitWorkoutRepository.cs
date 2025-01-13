using Domain.Base.Paginated;
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

public async Task<PaginatedExercisesDto> GetAllExercisesByGymId(int gymId, int? perPage, int? page, string orderBy, string order, string? search)
{
    perPage ??= 10;
    page ??= 0;
    var skip = perPage * page;

    var query = _contextBase.FitWorkout
        .Where(x => x.GymId == gymId || (gymId == 1 && x.GymId == null));

    if (!string.IsNullOrEmpty(search))
    {
        query = query.Where(x => x.WorkoutName.ToLower().Contains(search.ToLower()));
    }

    if (!string.IsNullOrEmpty(orderBy))
    {
        if (order?.ToLower() == "desc")
        {
            query = orderBy.ToLower() switch
            {
                "name" => query.OrderByDescending(x => x.WorkoutName),
                "id" => query.OrderByDescending(x => x.WorkoutId),
                _ => query.OrderByDescending(x => x.WorkoutId)
            };
        }
        else
        {
            query = orderBy.ToLower() switch
            {
                "name" => query.OrderBy(x => x.WorkoutName),
                "id" => query.OrderBy(x => x.WorkoutId),
                _ => query.OrderBy(x => x.WorkoutId)
            };
        }
    }
    else
    {
         query = query.OrderByDescending(x => x.WorkoutId);
    }

    // DTO after created the entire query
    var exercisesDtoQuery = query.Select(x => new GetExercisesDto(
        x.WorkoutId,
        x.WorkoutName,
        _secrets.Value.S3Url + x.S3Path,
        _secrets.Value.S3Url + x.ImgPath,
        x.Type
    ));

    var total = await query.CountAsync();
    var listExercises = await exercisesDtoQuery.Skip(skip ?? 0).Take(perPage ?? 10).ToListAsync();

    return new PaginatedExercisesDto(total, listExercises);
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