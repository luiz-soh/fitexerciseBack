namespace Application.UserWorkout.v2.Boundaries
{
    public class AddUserWorkoutInput
    {
        public int UserId { get; set; }
        public int? WorkoutId { get; set; }
        public int GroupId { get; set; }
        public string WorkoutName { get; set; } = string.Empty;
        public int? WorkoutSeries { get; set; }
        public string? WorkoutRepetition { get; set; }
        public int Position { get; set; } = 0;
    }
}