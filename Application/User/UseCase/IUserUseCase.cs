using Application.User.Boundaries.Input;
using Domain.Base.Paginated;
using Domain.DTOs.Token;
using Domain.DTOs.User;

namespace Application.User.UseCase
{
    public interface IUserUseCase
    {
        Task AddUserEmail(AddUserEmailInput input);
        Task<UserDto> GetUserData(int userId);

        Task<TokenDto> SignIn(SignInInput input);
        Task SignUp(SignUpInput input);

        Task<TokenDto> UpdateToken(UpdateTokenInput input);
        Task<bool> DeleteUser(int userId);
        Task<PaginatedDto<UserDto>> GetUsersByGymId(int gymId, int? perPage, int? page, string orderBy, string order, string? search);
        Task<bool> UserExists(string username, string email);
        Task<UserDto> GetRecoverCode(string email);
        Task<int> GetUserIdByRecoveryCode(string code);
        Task<bool> RecoverUserPassword(int userId, string password);
    }
}
