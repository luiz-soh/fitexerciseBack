namespace Application.UserWorkout.v2.Boundaries
{
    public class DynamoUserWorkoutOutput
    {
        public int Position { get; set; }
        public int? Series { get; set; }
        public string? Repetitions { get; set; }
        public int? WorkoutId { get; set; }
        public string? ImgUrl { get; set; }
        public string? VideoUrl { get; set; }
        public string WorkoutName { get; set; } = string.Empty;
    }
}