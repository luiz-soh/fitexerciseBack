using Application.Token.UseCase;
using Application.User.Boundaries.Input;
using Domain.Base.Communication;
using Domain.Base.Messages.CommonMessages.Notification;
using Domain.DTOs.Authentication;
using Domain.DTOs.Token;
using Domain.DTOs.User;
using Domain.Entities.User;
using System.Security.Cryptography;

namespace Application.User.UseCase
{

    public class UserUseCase(IUserRepository userRepository, IMediatorHandler handler,
        ITokenUseCase tokenUseCase) : IUserUseCase
    {

        private readonly IUserRepository _userRepository = userRepository;
        private readonly ITokenUseCase _tokenUseCase = tokenUseCase;
        private readonly IMediatorHandler _handler = handler;

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

        public async Task<UserDto> GetRecoverCode(string email)
        {
            var user = await _userRepository.GetUserByEmail(email);

            if (user.Id == 0)
            {
                await _handler.PublishNotification(new DomainNotification("credential", "E-mail não encontrado"));
                return new UserDto();
            }
            else
            {
                var code = _tokenUseCase.GenerateRecoveryCode();
                await _userRepository.AddPasswordRecoveryCode(user.Id, code);
                user.RecoverCode = code;
                return user;
            }
        }

        public async Task<bool> RecoverUserPassword(int userId, string password)
        {
            var encryptedPassword = _tokenUseCase.EncryptPassword(password);

            return await _userRepository.ChangeUserPassword(userId, encryptedPassword);
        }

        public async Task<int> GetUserIdByRecoveryCode(string code)
        {
            return await _userRepository.GetUserIdByRecoverCode(code);
        }

        #region private methods
        private TokenDto GenerateToken(UserDto user)
        {
            var token = _tokenUseCase.GenerateToken(user.Name, "adm", 24, user.Id);

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
