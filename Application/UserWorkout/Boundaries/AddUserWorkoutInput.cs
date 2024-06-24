namespace Application.UserWorkout.Boundaries
{
    public class AddUserWorkoutInput
    {
        public int UserId { get; set; } = 0;
        public int WorkoutId { get; set; } = 0;
        public int GroupWorkoutId { get; set; } = 0;
        public int? WorkoutSeries { get; set; }
        public string? WorkoutRepetition { get; set; }
    }
}