using Domain.Entities.GroupWorkout;

namespace Domain.DTOs.UserWorkout
{
    public class CheckInWorkoutDto
    {

        public CheckInWorkoutDto(int groupId, string duration, int userId)
        {
            GroupId = groupId;
            Duration = duration;
            UserId = userId;
            CheckInDate = DateTime.Now;
        }

        public CheckInWorkoutDto(CheckInWorkoutEntity entity)
        {
            CheckInDate = entity.CheckInDate;
            Duration = entity.Duration;
            GroupId = entity.GroupId;
            UserId = entity.UserId;
        }

        public DateTime CheckInDate { get; set; }
        public string Duration { get; set; } = string.Empty;
        public int GroupId { get; set; }
        public int UserId { get; set; }
    }
}