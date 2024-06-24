namespace Domain.DTOs.UserWorkout
{
    public class UpdateUserWorkoutDto
    {
        public UpdateUserWorkoutDto()
        {
            UserId = 0;
            WorkoutId = 0;
            GroupWorkoutId = 0;
            WorkoutPosition = 0;
            WorkoutSeries = null;
            WorkoutRepetitions = null;
            UwId = 0;
        }

        public UpdateUserWorkoutDto(int userId, int workoutId, int groupWorkoutId, int? series,
         string? repetitions, int position, int uwId) 
        {
            UserId = userId;
            WorkoutId = workoutId;
            GroupWorkoutId = groupWorkoutId;
            WorkoutSeries = series;
            WorkoutRepetitions = repetitions;
            WorkoutPosition = position;
            UwId = uwId;
        }

        public int UserId { get; set; }
        public int WorkoutId { get; set; }
        public int GroupWorkoutId { get; set; }
        public int UwId { get; set; }
        public int? WorkoutSeries { get; set; }
        public string? WorkoutRepetitions { get; set; }
        public int WorkoutPosition { get; set; }
    }
}