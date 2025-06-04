namespace Application.UserWorkout.v2.Boundaries
{
    public class SaveUserWorkoutInput
    {
        public int? WorkoutId { get; set; }
        public string WorkoutName { get; set; } = string.Empty;
        public int? Series { get; set; }
        public string? Repetitions { get; set; }
        public int Position { get; set; } = 0;
    }
}