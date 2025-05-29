namespace Application.UserWorkout.v2.Boundaries
{
    public class SaveUserWorkoutInput
    {
        public int? WorkoutId { get; set; }
        public string WorkoutName { get; set; } = string.Empty;
        public int? WorkoutSeries { get; set; }
        public string? WorkoutRepetition { get; set; }
        public int Position { get; set; } = 0;
    }
}