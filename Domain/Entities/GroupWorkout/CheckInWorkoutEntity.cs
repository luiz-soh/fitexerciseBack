using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.DTOs.UserWorkout;

namespace Domain.Entities.GroupWorkout
{
    [Table("checkin_workout")]
    public class CheckInWorkoutEntity
    {
        public CheckInWorkoutEntity()
        {
            Id = 0;
            CheckInDate = DateTime.Now;
            Duration = string.Empty;
            GroupId = 0;
            UserId = 0;
        }

        public CheckInWorkoutEntity(CheckInWorkoutDto dto)
        {
            Id = 0;
            CheckInDate = dto.CheckInDate;
            Duration = dto.Duration;
            GroupId = dto.GroupId;
            UserId = dto.UserId;
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("checkin_date")]
        public DateTime CheckInDate { get; set; }

        [Column("duration")]
        public string Duration { get; set; }

        [Column("group_id")]
        public int GroupId { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }
    }
}