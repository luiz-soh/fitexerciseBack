namespace Application.UserWorkout.v2.Boundaries
{
    public class AddUserWorkoutInput
    {
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public SaveUserWorkoutInput Workout { get; set; } = new SaveUserWorkoutInput();
    }
}