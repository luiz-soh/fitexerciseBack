namespace Domain.DTOs.UserWorkout
{
    public class ChangeUserWorkoutPositionDto
    {
        public ChangeUserWorkoutPositionDto()
        {
            WorkoutPosition = 0;
            UwId = 0;
        }

        public ChangeUserWorkoutPositionDto(int uwId, int position)
        {
            WorkoutPosition = position;
            UwId = uwId;
        }

        public int UwId { get; set; }
        public int WorkoutPosition { get; set; }
    }
}