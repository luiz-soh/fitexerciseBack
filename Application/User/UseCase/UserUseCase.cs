using Application.Token.UseCase;
using Application.User.Boundaries.Input;
using Domain.DTOs.Authentication;
using Domain.DTOs.Token;
using Domain.DTOs.User;
using Domain.Entities.User;
using System.Security.Cryptography;

namespace Application.User.UseCase
{

    public class UserUseCase : IUserUseCase
    {

        private readonly IUserRepository _userRepository;
        private readonly ITokenUseCase _tokenUseCase;


        public UserUseCase(IUserRepository userRepository,
            ITokenUseCase tokenUseCase)
        {
            _userRepository = userRepository;
            _tokenUseCase = tokenUseCase;
        }

        public async Task AddUserEmail(AddUserEmailInput input)
        {
            await _userRepository.AddUserEmail(input.Email, input.UserId);
        }

        public async Task<UserDto> GetUserData(int userId)
        {
            return await _userRepository.GetUserData(userId);
        }

        public async Task<TokenDto> SignIn(SignInInput input)
        {
            var loginDto = new SignInDto(input.Username, input.Password);

            var encryptedPassword = _tokenUseCase.EncryptPassword(input.Password);
            loginDto.Password = encryptedPassword;

            var user = await _userRepository.SignIn(loginDto);

            if (user.Id != 0)
            {
                var token = GenerateToken(user);

                await _userRepository.UpdateRefreshToken(user.Id, token.RefreshToken);

                return token;
            }
            return new TokenDto();
        }

        public async Task<bool> UserExists(string username, string email)
        {
            return await _userRepository.UserAlreadyExists(username, email);
        }

        public async Task SignUp(SignUpInput input)
        {

            var encryptedPassword = _tokenUseCase.EncryptPassword(input.Password);
            var signUp = new SignUpDto(input.Username, encryptedPassword, input.UserEmail, input.GymId, input.UserProfile);
            await _userRepository.SignUp(signUp);

            //Mandar e-mail via SES quando produto estiver finalizado

        }

        public async Task<TokenDto> UpdateToken(UpdateTokenInput input)
        {

            var user = await _userRepository.GetRefreshToken(input.RefreshToken, input.UserId);

            if (user.Id != 0)
            {

                var token = GenerateToken(user);


                await _userRepository.UpdateRefreshToken(user.Id, token.RefreshToken);

                return token;
            }

            return new TokenDto();

        }

        public async Task<bool> DeleteUser(int userId)
        {
            return await _userRepository.DeleteUser(userId);
        }

        public async Task<List<UserDto>> GetUsersByGymId(int gymId)
        {
            return await _userRepository.GetUsersByGymId(gymId);
        }


        #region private methods
        private TokenDto GenerateToken(UserDto user)
        {
            var token = _tokenUseCase.GenerateToken(user.Name, "adm", 24);

            return new TokenDto(token, GenerateRefreshToken(), user.Id);
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        #endregion
    }
}
