using Domain.Entities.FitWorkout;

namespace Domain.DTOs.FitWorkout
{
    public class GetExerciseByIdDto
    {
        #region constructors
        public GetExerciseByIdDto()
        {
            WorkoutId = 0;
            S3Path = string.Empty;
            WorkoutName = string.Empty;
            ImgPath = string.Empty;
            FileName = string.Empty;
            GymId = null;
        }

        public GetExerciseByIdDto(FitWorkoutEntity workout)
        {
            S3Path = workout.S3Path;
            WorkoutName = workout.WorkoutName;
            ImgPath = workout.ImgPath;
            WorkoutId = workout.WorkoutId;
            GymId = workout.GymId;
            FileName = workout.FileName;
        }

        public GetExerciseByIdDto(FitWorkoutEntity workout, string videoPath, string imgPath)
        {
            S3Path = videoPath;
            WorkoutName = workout.WorkoutName;
            ImgPath = imgPath;
            WorkoutId = workout.WorkoutId;
            FileName = workout.FileName;
            GymId = workout.GymId;
        }

        #endregion

        public int WorkoutId { get; set; }
        public string S3Path { get; set; }
        public string WorkoutName { get; set; }
        public string ImgPath { get; set; }
        public int? GymId { get; set; }
        public string FileName { get; set; }
    }
}
