namespace Application.GroupWorkout.Boundaries
{
    public class CreateGroupInput
    {
        public CreateGroupInput()
        {
            Name = string.Empty;
            UserId = 0;

        }

        public string Name { get; set; }
        public int UserId { get; set; }
    }
}