namespace Application.Gym.Boundaries
{
        public class LoginInput
    {
        public LoginInput()
        {
            Login = string.Empty;
            Password= string.Empty;
        }

        public string Login { get; set; }
        public string Password { get; set; }
    }
}