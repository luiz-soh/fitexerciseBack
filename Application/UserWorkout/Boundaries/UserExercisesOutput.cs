using Domain.DTOs.UserWorkout;

namespace Application.UserWorkout.Boundaries
{
    public class UserExerciseOutput
    {
        public UserExerciseOutput(UserExercisesDto dto)
        {
            Id = dto.Id;
            Name = dto.Name;
            S3Path = dto.S3Path;
            ImgPath = dto.ImgPath;
            UwId = dto.UwId;
            WorkoutSeries = dto.WorkoutSeries;
            WorkoutPosition = dto.WorkoutPosition;
            WorkoutRepetitions = dto.WorkoutRepetitions;
            GroupId = dto.GroupId;
        }

        public UserExerciseOutput()
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
    }
}
