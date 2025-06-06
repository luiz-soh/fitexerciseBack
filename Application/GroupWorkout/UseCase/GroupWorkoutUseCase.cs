using Domain.Base.Communication;
using Domain.Base.Messages.CommonMessages.Notification;
using Domain.DTOs.GroupWorkout;
using Domain.DTOS.GroupWorkout;
using Domain.Entities.GroupWorkout;

namespace Application.GroupWorkout.UseCase
{
    public class GroupWorkoutUseCase : IGroupWorkoutUseCase
    {
        private readonly IGroupWorkoutRepository _groupWorkoutRepository;
        private readonly IMediatorHandler _mediatorHandler;
        public GroupWorkoutUseCase(IGroupWorkoutRepository groupWorkoutRepository, IMediatorHandler handler)
        {
            _groupWorkoutRepository = groupWorkoutRepository;
            _mediatorHandler = handler;
        }

        public async Task AddCheckinWorkout(CheckInWorkoutDto dto)
        {
            var group = await _groupWorkoutRepository.GetGroupById(dto.GroupId);
            if (group is not null && group.UserId == dto.UserId)
            {
                await _groupWorkoutRepository.AddCheckInWorkout(dto);
            }
            else
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("error:", "Grupo não econtrado"));
            }
        }

        public async Task<bool> AddGroupWorkout(int userId, string name)
        {
            await _groupWorkoutRepository.CreateGroupWorkout(userId, name);
            return true;
        }

        public async Task<bool> DeleteGroupWorkout(int id, int userId)
        {
            await _groupWorkoutRepository.DeleteGroupWorkout(id, userId);
            return true;
        }

        public async Task<GroupWorkoutDto> GetGroupById(int id)
        {
            return await _groupWorkoutRepository.GetGroupById(id);
        }

        public async Task<List<GroupWorkoutDto>> GetGroups(int userId)
        {
            return await _groupWorkoutRepository.ListGroupWorkout(userId);
        }

        public async Task<bool> UpdateGroupWorkout(int id, string name)
        {
            await _groupWorkoutRepository.UpdateGroupWorkout(id, name);
            return true;
        }
    }
}