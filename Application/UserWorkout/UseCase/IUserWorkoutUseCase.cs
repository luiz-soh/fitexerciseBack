using Application.UserWorkout.Boundaries;
using Application.UserWorkout.v2.Boundaries;
using Domain.DTOs.UserWorkout;

namespace Application.UserWorkout.UseCase
{
    public interface IUserWorkoutUseCase
    {
        Task AddUserWorkout(AddUserWorkoutDto input);
        Task SaveUserWorkout(int userId, int groupId, DynamoUserWorkoutDto input);
        Task UpdateUserWorkouts(int userId, int groupId, List<DynamoUserWorkoutDto> input);
        Task<List<DynamoUserWorkoutDto>> GetUserWorkouts(int userId, int groupId);
        Task<List<UserExercisesDto>> GetUserExercises(int userId, int groupId);
        Task DeleteUserWorkoutId(int userId, int workoutId);
        Task ChangeUserWorkoutPosition(List<UserWorkoutPositionInput> input);
        Task UpdateUserWorkout(UpdateUserWorkoutDto dto);
        Task AddCheckinWorkout(CheckInWorkoutDto dto);
        Task<List<CheckInWorkoutDto>> ListCheckinsWorkout(int userId);
    }
}