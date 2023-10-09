using Domain.Enums;

namespace Domain.DTOs.Authentication
{
    public class SignInDto
    {

        public string Username { get; set; }
        public string Password { get; set; }
        public UserProfileEnum UserProfile { get; set; }

        #region construtores
        public SignInDto()
        {
            Username = string.Empty;
            Password = string.Empty;
            UserProfile = UserProfileEnum.user;

        }

        public SignInDto(string username, string password, int userProfile)
        {
            Username = username;
            Password = password;
            UserProfile = (UserProfileEnum)userProfile;
        }

        public SignInDto(string username, string password)
        {
            Username = username;
            Password = password;
            UserProfile = UserProfileEnum.user;
        }
        #endregion
    }
}
