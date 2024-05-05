namespace Application.FitWorkout.Boundaries
{
    public class EditExerciseDataInput
    {
        public EditExerciseDataInput()
        {
            ExerciseName = string.Empty;
            WorkoutId = 0;
        }

        public int WorkoutId { get; set; }
        public string ExerciseName { get; set; }
        public int? GymId { get; set; }
    }
}