using Domain.Entities.GroupWorkout;

namespace Domain.DTOs.GroupWorkout
{
    public class GroupWorkoutDto
    {

        public GroupWorkoutDto()
        {
            Id = 0;
            Name = string.Empty;
            UserId = 0;
        }

        public GroupWorkoutDto(GroupWorkoutEntity entity)
        {
            Id = entity.GroupId;
            Name = entity.GroupName;
            UserId = entity.UserId;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
    }
}