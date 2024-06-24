using Domain.DTOs.UserWorkout;

namespace Domain.Entities.UserWorkout
{
     public interface IUserWorkoutRepository
    {
        Task AddUserWorkout(AddUserWorkoutDto input);
        Task<List<UserExercisesDto>> GetUserExercises(int userId, int groupId);
        Task DeleteUserWorkoutId(int userId, int workoutId);
        Task ChangeUserWorkoutPosition(List<ChangeUserWorkoutPositionDto> input);
        Task UpdateUserWorkout(UpdateUserWorkoutDto input);
    }
}