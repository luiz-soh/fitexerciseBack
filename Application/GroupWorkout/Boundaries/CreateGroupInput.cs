namespace Application.GroupWorkout.Boundaries
{
    public class CreateGroupInput
    {
        public CreateGroupInput()
        {
            Name = string.Empty;

        }

        public string Name { get; set; }
    }
}