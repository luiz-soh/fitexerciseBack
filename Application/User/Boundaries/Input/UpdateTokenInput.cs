namespace Application.User.Boundaries.Input
{
    public class UpdateTokenInput
    {
        public string RefreshToken { get; set; }

        public int UserId { get; set; }

        public UpdateTokenInput()
        {
            RefreshToken = string.Empty;
            UserId = 0;
        }
    }
}
