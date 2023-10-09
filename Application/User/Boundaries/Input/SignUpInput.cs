using Domain.Enums;

namespace Application.User.Boundaries.Input
{
    public class SignUpInput
    {
        public SignUpInput()
        {
            Username = string.Empty;
            Password = string.Empty;
            ConfirmPassword = string.Empty;
            UserProfile = (int)UserProfileEnum.user;
            UserEmail = null;
            GymId = null;
        }

        public string Username { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public int UserProfile { get; set; }

        public string? UserEmail { get; set; }

        public int? GymId { get; set; }
    }
}
