using Domain.Enums;

namespace Domain.DTOs.Authentication
{
    public class SignUpDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string? UserEmail { get; set; }
        public UserProfileEnum UserProfile { get; set; }
        public int? GymId { get; set; }


        #region construtores
        public SignUpDto()
        {
            Username = string.Empty;
            Password = string.Empty;
            UserEmail = null;
            UserProfile = UserProfileEnum.user;
            GymId = null;

        }

        public SignUpDto(string username, string encryptedPassword, string? email, int? gymId, int userProfile)
        {
            Username = username;
            Password = encryptedPassword;
            UserProfile = (UserProfileEnum)userProfile;
            UserEmail = email;
            GymId = gymId;
        }
        #endregion
    }
}
