namespace Application.UserWorkout.Boundaries
{
    public class UpdateUserWorkoutInput
    {
        public int UserId { get; set; }

        public int WorkoutId { get; set; }

        public int GroupWorkoutId { get; set; }

        public int? WorkoutSeries { get; set; }

        public string? WorkoutRepetition { get; set; }
        public int WorkoutPosition { get; set; }
        public int UwId { get; set; }
    }
}
