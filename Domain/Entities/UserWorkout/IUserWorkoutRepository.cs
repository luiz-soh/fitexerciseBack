using Domain.DTOs.UserWorkout;

namespace Domain.Entities.UserWorkout
{
    public interface IUserWorkoutRepository
    {
        Task AddUserWorkout(AddUserWorkoutDto input);
        Task SaveUserWorkout(int groupId, DynamoUserWorkoutDto dto);
        Task UpdateUserWorkouts(int groupId, List<DynamoUserWorkoutDto> dtos);
        Task<List<DynamoUserWorkoutDto>> GetUserWorkouts(int groupId);
        Task<List<UserExercisesDto>> GetUserExercises(int userId, int groupId);
        Task DeleteUserWorkoutId(int userId, int workoutId);
        Task ChangeUserWorkoutPosition(List<ChangeUserWorkoutPositionDto> input);
        Task UpdateUserWorkout(UpdateUserWorkoutDto input);
        Task AddCheckInWorkout(CheckInWorkoutDto dto);
        Task<List<CheckInWorkoutDto>> ListCheckInsWorkout(int userId);
    }
}