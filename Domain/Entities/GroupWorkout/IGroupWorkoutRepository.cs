using Domain.DTOs.GroupWorkout;
using Domain.DTOS.GroupWorkout;

namespace Domain.Entities.GroupWorkout
{
    public interface IGroupWorkoutRepository
    {
        Task<List<GroupWorkoutDto>> ListGroupWorkout(int userId);
        Task CreateGroupWorkout(int userId, string name);
        Task<GroupWorkoutDto> GetGroupById(int id);
        Task UpdateGroupWorkout(int id, string name);
        Task DeleteGroupWorkout(int id, int userId);
        Task AddCheckInWorkout(CheckInWorkoutDto dto);
    }
}