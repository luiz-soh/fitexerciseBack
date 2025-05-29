namespace Application.UserWorkout.v2.Boundaries
{
    public class UpdateUserWorkoutsInput
    {
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public List<SaveUserWorkoutInput>? Workouts { get; set; }
    }
}