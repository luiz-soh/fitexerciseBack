using Domain.Enums;

namespace Domain.DTOs.UserWorkout
{
    public class UserExercisesDto
    {
        public UserExercisesDto(int workoutId, string name, string s3Path,
            string imgPath, int uwId, int? workoutSeries, string? workoutRepetitions,
            int workoutPosition, int groupId, ExerciseTypeEnum type)
        {
            Id = workoutId;
            Name = name;
            S3Path = s3Path;
            ImgPath = imgPath;
            UwId = uwId;
            WorkoutSeries = workoutSeries;
            WorkoutPosition = workoutPosition;
            WorkoutRepetitions = workoutRepetitions;
            GroupId = groupId;
            Type = type;
        }

        public UserExercisesDto()
        {
            Id = 0;
            Name = string.Empty;
            S3Path = string.Empty;
            ImgPath = string.Empty;
            UwId = 0;
            WorkoutSeries = null;
            WorkoutRepetitions = null;
            WorkoutPosition = 0;
            GroupId = 0;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string S3Path { get; set; }
        public string ImgPath { get; set; }
        public int UwId { get; set; }
        public int? WorkoutSeries { get; set; }
        public string? WorkoutRepetitions { get; set; }
        public int WorkoutPosition { get; set; }
        public int GroupId { get; set; }
        public ExerciseTypeEnum Type { get; set; }
    }
}
