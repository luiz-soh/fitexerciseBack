namespace Domain.DTOs.UserWorkout
{
    public class SaveUserWorkoutDto
    {
        public SaveUserWorkoutDto()
        {
            GroupId = 0;
        }

        public SaveUserWorkoutDto(int groupId, int? series, string? repetitions, int? workoutId)
        {
            GroupId = groupId;
            Series = series;
            Repetitions = repetitions;
            WorkoutId = workoutId;
        }

        public int GroupId { get; set; }
        public int? Series { get; set; }
        public string? Repetitions { get; set; }
        public int? WorkoutId { get; set; }
        public string? ImgUrl { get; set; }
        public string? VideoUrl { get; set; }
    }
}