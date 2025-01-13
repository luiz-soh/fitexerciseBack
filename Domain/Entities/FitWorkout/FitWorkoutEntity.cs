using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Domain.DTOs.FitWorkout;
using Domain.Enums;

namespace Domain.Entities.FitWorkout
{
    [Table("fit_workout")]
    public class FitWorkoutEntity
    {
        public FitWorkoutEntity(FitWorkoutDto fitworkout)
        {
            S3Path = fitworkout.S3Path;
            WorkoutName = fitworkout.WorkoutName;
            WorkoutId = fitworkout.WorkoutId;
            FileName = fitworkout.FileName;
            ImgPath = fitworkout.ImgPath;
            GymId = fitworkout.GymId;
            Type = fitworkout.Type;
        }

        public FitWorkoutEntity()
        {
            WorkoutId = 0;
            S3Path = string.Empty;
            WorkoutName = string.Empty;
            FileName = string.Empty;
            ImgPath = string.Empty;
            GymId = null;
        }

        [Column("workout_id")]
        [Key]
        public int WorkoutId { get; set; }

        [Column("s3_path")]
        public string S3Path { get; set; }

        [Column("workout_name")]
        public string WorkoutName { get; set; }

        [Column("file_name")]
        public string FileName { get; set; }

        [Column("img_path")]
        public string ImgPath { get; set; }

        [Column("gym_id")]
        public int? GymId { get; set; }

        [Column("workout_type")]
        public ExerciseTypeEnum Type { get; set; }
    }
}
