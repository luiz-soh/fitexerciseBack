using System.Text.Json;
using Amazon.DynamoDBv2.DataModel;
using Domain.Configuration;
using Domain.DTOs.UserWorkout;
using Domain.Entities.UserWorkout;
using Infra.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infra.Repository.UserWorkout
{
    public class UserWorkoutRepository : IUserWorkoutRepository
    {
        private readonly DbContextOptions<ContextBase> _optionsBuilder;
        private readonly IOptions<Secrets> _secrets;
        private readonly IDynamoDBContext _dinamoDBContext;
        public UserWorkoutRepository(IOptions<Secrets> secrets, IDynamoDBContext dynamoDBContext)
        {
            _secrets = secrets;
            _optionsBuilder = new DbContextOptions<ContextBase>();
            _dinamoDBContext = dynamoDBContext;
        }

        public async Task AddUserWorkout(AddUserWorkoutDto input)
        {
            using var context = new ContextBase(_optionsBuilder, _secrets);
            var userWorkout = new UserWorkoutEntity(input);

            await context.UserWorkout.AddAsync(userWorkout);
            await context.SaveChangesAsync();
        }

        public async Task DeleteUserWorkoutId(int userId, int workoutId)
        {
            using var context = new ContextBase(_optionsBuilder, _secrets);
            var userWorkout = await context.UserWorkout.Where(x => x.UserId == userId && x.WorkoutId == workoutId).FirstOrDefaultAsync();
            if (userWorkout != null)
            {
                context.UserWorkout.Remove(userWorkout);
                await context.SaveChangesAsync();
            }
        }

        // Queremos deixar duplicar se mandar duas vezes o mesmo SaveUserWorkoutDto
        public async Task SaveUserWorkout(int groupId, SaveUserWorkoutDto dto)
        {
            if (dto.WorkoutId is not null)
            {
                using var context = new ContextBase(_optionsBuilder, _secrets);
                var workout = await context.FitWorkout.Where(x => x.WorkoutId == dto.WorkoutId).AsNoTracking().FirstOrDefaultAsync();
                if (workout is not null)
                {
                    dto.ImgUrl = _secrets.Value.S3Url + workout.ImgPath;
                    dto.VideoUrl = _secrets.Value.S3Url + workout.S3Path;
                    dto.WorkoutName = workout.WorkoutName;
                }
            }
            List<SaveUserWorkoutDto> workoutPlan = [];
            workoutPlan.Add(dto);
            var dynamoWorkout = await _dinamoDBContext.LoadAsync<DynamoUserWorkout>(groupId);
            if (dynamoWorkout is not null)
            {
                var currentPlan = JsonSerializer.Deserialize<List<SaveUserWorkoutDto>>(dynamoWorkout.WorkoutPlan);
                if (currentPlan is not null)
                    workoutPlan.AddRange(currentPlan);
            }

            var plan = JsonSerializer.Serialize(workoutPlan);
            var entity = new DynamoUserWorkout(groupId, plan);
            await _dinamoDBContext.SaveAsync(entity);
        }

        public async Task UpdateUserWorkouts(int groupId, List<SaveUserWorkoutDto> dtos)
        {
            List<SaveUserWorkoutDto> workoutPlan = [];
            foreach (var dto in dtos)
            {
                if (dto.WorkoutId is not null)
                {
                    using var context = new ContextBase(_optionsBuilder, _secrets);
                    var workout = await context.FitWorkout.Where(x => x.WorkoutId == dto.WorkoutId).AsNoTracking().FirstOrDefaultAsync();
                    if (workout is not null)
                    {
                        dto.ImgUrl = _secrets.Value.S3Url + workout.ImgPath;
                        dto.VideoUrl = _secrets.Value.S3Url + workout.S3Path;
                        dto.WorkoutName = workout.WorkoutName;
                    }
                }

                workoutPlan.Add(dto);
            }

            var plan = JsonSerializer.Serialize(workoutPlan);
            var entity = new DynamoUserWorkout(groupId, plan);
            await _dinamoDBContext.SaveAsync(entity);
        }

        public async Task<List<UserExercisesDto>> GetUserExercises(int userId, int groupId)
        {
            using var context = new ContextBase(_optionsBuilder, _secrets);

            return await (from uW in context.UserWorkout
                          join workout in context.FitWorkout on uW.WorkoutId equals workout.WorkoutId
                          where uW.UserId == userId && uW.GroupWorkoutId == groupId
                          orderby uW.WorkoutPosition
                          select new
                          UserExercisesDto(
                           workout.WorkoutId,
                           workout.WorkoutName,
                           _secrets.Value.S3Url + workout.S3Path,
                           _secrets.Value.S3Url + workout.ImgPath,
                           uW.UwId,
                           uW.WorkoutSeries,
                           uW.WorkoutRepetitions,
                           uW.WorkoutPosition,
                           groupId,
                           workout.Type
                          )).AsNoTracking().ToListAsync();
        }

        public async Task ChangeUserWorkoutPosition(List<ChangeUserWorkoutPositionDto> input)
        {
            using var context = new ContextBase(_optionsBuilder, _secrets);

            var workoutIds = input.Select(e => e.UwId).ToList();

            var userWorkouts = await context.UserWorkout.Where(e => workoutIds.Contains(e.UwId)
                ).AsNoTracking().ToListAsync();

            var workoutsToChange = (from uw in userWorkouts
                                    join workoutsChange in input on uw.UwId equals workoutsChange.UwId
                                    select new UserWorkoutEntity(uw, workoutsChange.WorkoutPosition)).ToList();
            if (workoutsToChange != null)
            {
                context.UserWorkout.UpdateRange(workoutsToChange);
                await context.SaveChangesAsync();
            }
        }

        public async Task UpdateUserWorkout(UpdateUserWorkoutDto input)
        {
            using var context = new ContextBase(_optionsBuilder, _secrets);
            var userWorkout = new UserWorkoutEntity(input);

            context.UserWorkout.Update(userWorkout);
            await context.SaveChangesAsync();
        }
    }
}