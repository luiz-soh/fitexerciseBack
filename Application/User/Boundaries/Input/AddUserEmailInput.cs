namespace Application.User.Boundaries.Input
{
    public class AddUserEmailInput
    {
        public AddUserEmailInput()
        {
            Email = string.Empty;
            UserId = 0;
        }

        public string Email { get; set; }

        public int UserId { get; set; }
    }
}
