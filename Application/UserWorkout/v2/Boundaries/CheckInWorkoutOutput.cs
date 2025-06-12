namespace Application.UserWorkout.v2.Boundaries
{
    public class CheckInWorkoutOutput
    {
        public DateTime CheckInDate { get; set; }
        public string Duration { get; set; } = string.Empty;
        public int GroupId { get; set; }
        public int UserId { get; set; }
    }
}