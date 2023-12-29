using Domain.DTOs.Authentication;
using Domain.DTOs.User;

namespace Domain.Entities.User
{
    public interface IUserRepository
    {
        Task<UserDto> SignIn(SignInDto input);
        Task SignUp(SignUpDto input);
        Task UpdateRefreshToken(int userId, string refreshToken);
        Task<UserDto> GetRefreshToken(string refreshToken, int userId);
        Task<bool> UserAlreadyExists(string username, string email);
        Task<bool> DeleteUser(int userId);
        Task AddUserEmail(string email, int userId);
        Task<UserDto> GetUserData(int userId);
        Task<List<UserDto>> GetUsersByGymId(int gymId);
    }
}
