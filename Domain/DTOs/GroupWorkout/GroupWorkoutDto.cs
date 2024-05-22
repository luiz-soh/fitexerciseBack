using Domain.Entities.GroupWorkout;

namespace Domain.DTOs.GrouptWorkout
{
    public class GroupWorkoutDto
    {

        public GroupWorkoutDto()
        {
            Id = 0;
            Name = string.Empty;
        }

        public GroupWorkoutDto(GroupWorkoutEntity entity)
        {
            Id = entity.GroupId;
            Name = entity.GroupName;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}