namespace Domain.DTOs.Token
{
    public class TokenDto
    {
        #region  construtores
        public TokenDto(string userToken, string refreshToken, int userId)
        {
            UserToken = userToken;
            RefreshToken = refreshToken;
            UserId = userId;
        }

        public TokenDto()
        {
            UserToken = string.Empty;
            RefreshToken = string.Empty;
            UserId = 0;
        }

        #endregion

        public int UserId { get; set; }
        public string UserToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
