using Domain.Base.Paginated;
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
        Task<PaginatedDto<UserDto>> GetUsersByGymId(int gymId, int? perPage, int? page, string orderBy, string order, string? search);
        Task<UserDto> GetUserByEmail(string email);
        Task<int> GetUserIdByRecoverCode(string code);
        Task<bool> AddPasswordRecoveryCode(int id, string code);
        Task<bool> ChangeUserPassword(int userId, string password);
        Task<bool> UserBelongsToGym(int userId, int gymId);
    }
}
