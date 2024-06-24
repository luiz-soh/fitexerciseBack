using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Domain.DTOs.UserWorkout;

namespace Domain.Entities.UserWorkout
{
    [Table("user_workout")]
    public class UserWorkoutEntity
    {
        public UserWorkoutEntity()
        {
            UwId = 0;
            UserId = 0;
            WorkoutId = 0;
            GroupWorkoutId = 0;
            WorkoutPosition = 0;
            WorkoutSeries = null;
            WorkoutRepetitions = null;
        }

        public UserWorkoutEntity(AddUserWorkoutDto dto)
        {
            UwId = 0;
            UserId = dto.UserId;
            WorkoutId = dto.WorkoutId;
            GroupWorkoutId = dto.GroupWorkoutId;
            WorkoutSeries = dto.WorkoutSeries;
            WorkoutRepetitions = dto.WorkoutRepetitions;
        }

        public UserWorkoutEntity(UpdateUserWorkoutDto dto)
        {
            UwId = dto.UwId;
            UserId = dto.UserId;
            WorkoutId = dto.WorkoutId;
            GroupWorkoutId = dto.GroupWorkoutId;
            WorkoutSeries = dto.WorkoutSeries;
            WorkoutRepetitions = dto.WorkoutRepetitions;
            WorkoutPosition = dto.WorkoutPosition;
        }

        public UserWorkoutEntity(UserWorkoutEntity entity, int position)
        {
            UwId = entity.UwId;
            UserId = entity.UserId;
            WorkoutId = entity.WorkoutId;
            GroupWorkoutId = entity.GroupWorkoutId;
            WorkoutSeries = entity.WorkoutSeries;
            WorkoutRepetitions = entity.WorkoutRepetitions;
            WorkoutPosition = position;
        }

        [Column("user_workout_id")]
        [Key]
        public int UwId { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("workout_id")]
        public int WorkoutId { get; set; }

        [Column("group_workout_id")]
        public int GroupWorkoutId { get; set; }

        [Column("user_workout_series")]
        public int? WorkoutSeries { get; set; }

        [Column("user_workout_repetitions")]
        public string? WorkoutRepetitions { get; set; }

        [Column("user_workout_position")]
        public int WorkoutPosition { get; set; }
    }
}
