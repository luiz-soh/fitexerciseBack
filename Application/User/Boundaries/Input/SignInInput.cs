namespace Application.User.Boundaries.Input
{
    public class SignInInput
    {
        public SignInInput()
        {
            Username = string.Empty;
            Password = string.Empty;

        }

        public string Username { get; set; }

        public string Password { get; set; }

    }

}
