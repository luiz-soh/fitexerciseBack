using Domain.DTOs.GroupWorkout;

namespace Application.GroupWorkout.UseCase
{
    public interface IGroupWorkoutUseCase
    {
        Task<bool> AddGroupWorkout(int userId, string name);
        Task<List<GroupWorkoutDto>> GetGroups(int userId);
        Task<GroupWorkoutDto> GetGroupById(int id);
        Task<bool> UpdateGroupWorkout(int id, string name);
        Task<bool> DeleteGroupWorkout(int id, int userId);
    }
}