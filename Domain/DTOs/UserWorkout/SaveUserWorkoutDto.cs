namespace Domain.DTOs.UserWorkout
{
    public class SaveUserWorkoutDto
    {
        public SaveUserWorkoutDto()
        {
            Position = 0;
            WorkoutName = string.Empty;
        }

        public SaveUserWorkoutDto(int position, int? series,
         string? repetitions, int? workoutId, string workoutName)
        {
            Position = position;
            Series = series;
            Repetitions = repetitions;
            WorkoutId = workoutId;
            WorkoutName = workoutName;
        }

        public int Position { get; set; }
        public int? Series { get; set; }
        public string? Repetitions { get; set; }
        public int? WorkoutId { get; set; }
        public string? ImgUrl { get; set; }
        public string? VideoUrl { get; set; }
        public string WorkoutName { get; set; }
    }
}