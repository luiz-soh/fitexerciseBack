
using Domain.DTOs.S3;
using Domain.Entities.FitWorkout;
using Domain.Enums;

namespace Domain.DTOs.FitWorkout
{
    public class FitWorkoutDto
    {
        #region constructors
        public FitWorkoutDto(UploadDto upload, int? gymId, int workoutId)
        {
            S3Path = upload.VideoPath;
            WorkoutName = upload.WorkoutName;
            WorkoutId = workoutId;
            FileName = upload.FileName;
            ImgPath = upload.ImgPath;
            GymId = gymId;
            Type = upload.Type;
        }

        public FitWorkoutDto(GetExerciseByIdDto input, string s3NewPath, string imgNewPath)
        {
            S3Path = s3NewPath;
            WorkoutName = input.WorkoutName;
            WorkoutId = input.WorkoutId;
            ImgPath = imgNewPath;
            GymId = input.GymId;
            FileName = input.FileName;
            Type = input.Type;
        }

        public FitWorkoutDto()
        {
            WorkoutId = 0;
            S3Path = string.Empty;
            WorkoutName = string.Empty;
            FileName = string.Empty;
            ImgPath = string.Empty;
            GymId = null;
            Type = ExerciseTypeEnum.Peito;
        }

        public FitWorkoutDto(FitWorkoutEntity workout)
        {
            S3Path = workout.S3Path;
            WorkoutName = workout.WorkoutName;
            WorkoutId = workout.WorkoutId;
            FileName = workout.FileName;
            ImgPath = workout.ImgPath;
            GymId = workout.GymId;
            Type = workout.Type;
        }

        #endregion

        public int WorkoutId { get; set; }
        public string S3Path { get; set; }
        public string WorkoutName { get; set; }
        public string FileName { get; set; }
        public string ImgPath { get; set; }
        public int? GymId { get; set; }
        public ExerciseTypeEnum Type { get; set; }
    }
}
