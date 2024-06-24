namespace Domain.DTOs.UserWorkout
{
    public class AddUserWorkoutDto
    {
        public AddUserWorkoutDto()
        {
            UserId = 0;
            WorkoutId = 0;
            GroupWorkoutId = 0;
            WorkoutSeries = null;
            WorkoutRepetitions = null;
        }

        public AddUserWorkoutDto(int userId, int workoutId, int groupWorkoutId, int? series, string? repetitions) 
        {
            UserId = userId;
            WorkoutId = workoutId;
            GroupWorkoutId = groupWorkoutId;
            WorkoutSeries = series;
            WorkoutRepetitions = repetitions;
        }

        public int UserId { get; set; }
        public int WorkoutId { get; set; }
        public int GroupWorkoutId { get; set; }

        public int? WorkoutSeries { get; set; }
        public string? WorkoutRepetitions { get; set; }
    }
}
