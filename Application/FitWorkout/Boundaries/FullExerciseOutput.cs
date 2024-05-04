using Domain.DTOs.FitWorkout;

namespace Application.FitWorkout.Boundaries
{
    public class FullExerciseOutput
    {
        #region constructors
        public FullExerciseOutput()
        {
            WorkoutId = 0;
            S3Path = string.Empty;
            WorkoutName = string.Empty;
            ImgPath = string.Empty;
            FileName = string.Empty;
            GymId = null;
        }

        public FullExerciseOutput(GetExerciseByIdDto workout)
        {
            S3Path = workout.S3Path;
            WorkoutName = workout.WorkoutName;
            ImgPath = workout.ImgPath;
            WorkoutId = workout.WorkoutId;
            GymId = workout.GymId;
            FileName = workout.FileName;
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
