namespace Application.GroupWorkout.Boundaries
{
    public class UpdateGroupInput
    {
        public UpdateGroupInput()
        {
            Id = 0;
            Name = string.Empty;
            UserId = 0;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
    }
}