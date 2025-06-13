using Domain.DTOs.GroupWorkout;

namespace Application.GroupWorkout.Boundaries
{
    public class GroupWorkoutOutput
    {
        public GroupWorkoutOutput()
        {
            Id = 0;
            Name = string.Empty;
        }

        public GroupWorkoutOutput(GroupWorkoutDto dto)
        {
            Id = dto.Id;
            Name = dto.Name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}