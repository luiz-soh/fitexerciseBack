
using Domain.Entities.FitWorkout;

namespace Domain.DTOs.FitWorkout
{
    public class FitWorkoutDto
    {
        #region constructors
        public FitWorkoutDto(string s3Path, string imgPath, string workoutName, string fileName, int? gymId)
        {
            S3Path = s3Path;
            WorkoutName = workoutName;
            WorkoutId = 0;
            FileName = fileName;
            ImgPath = imgPath;
            GymId = gymId;
        }

        public FitWorkoutDto(GetExerciseByIdDto input, string s3NewPath, string imgNewPath)
        {
            S3Path = s3NewPath;
            WorkoutName = input.WorkoutName;
            WorkoutId = input.WorkoutId;
            ImgPath = imgNewPath;
            GymId = input.GymId;
            FileName = input.FileName;
        }

        public FitWorkoutDto()
        {
            WorkoutId = 0;
            S3Path = string.Empty;
            WorkoutName = string.Empty;
            FileName = string.Empty;
            ImgPath = string.Empty;
            GymId = null;
        }

        public FitWorkoutDto(FitWorkoutEntity workout)
        {
            S3Path = workout.S3Path;
            WorkoutName = workout.WorkoutName;
            WorkoutId = workout.WorkoutId;
            FileName = workout.FileName;
            ImgPath = workout.ImgPath;
            GymId = workout.GymId;
        }

        #endregion

        public int WorkoutId { get; set; }
        public string S3Path { get; set; }
        public string WorkoutName { get; set; }
        public string FileName { get; set; }
        public string ImgPath { get; set; }
        public int? GymId { get; set; }
    }
}
