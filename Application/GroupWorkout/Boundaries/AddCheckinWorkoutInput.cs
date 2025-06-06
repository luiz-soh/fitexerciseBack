namespace Application.GroupWorkout.Boundaries
{
    public class AddCheckInWorkoutInput
    {
        public string Duration { get; set; } = string.Empty;

        public int GroupId { get; set; }
    }
}