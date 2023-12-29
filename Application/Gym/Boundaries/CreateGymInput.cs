namespace Application.Gym.Boundaries
{
    public class CreateGymInput
    {
        public CreateGymInput()
        {
            GymName = string.Empty;
            PlanId = 0;
            Password = string.Empty;
            Login = string.Empty;
            Email = string.Empty;
            ConfirmPassword = string.Empty;
        }

        public string GymName { get; set; }
        public int PlanId { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
    }
}