namespace Application.UserWorkout.v2.Boundaries
{
    public class AddUserWorkoutInput
    {
        public int UserId { get; set; }
        public int? WorkoutId { get; set; }
        public int GroupId { get; set; }
        public string? WorkoutName { get; set; }
        public int? WorkoutSeries { get; set; }
        public string? WorkoutRepetition { get; set; }
    }
}