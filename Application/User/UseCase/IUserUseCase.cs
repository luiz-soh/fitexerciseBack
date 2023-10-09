using Application.User.Boundaries.Input;
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
        Task<List<UserDto>> GetUsersByGymId(int gymId);

        Task<bool> UsernameExists(string username);
    }
}
