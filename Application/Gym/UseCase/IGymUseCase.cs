using Application.Gym.Boundaries;
using Domain.DTOs.Gym;


namespace Application.Gym.UseCase
{
    public interface IGymUseCase
    {
        Task<bool> GymExists(string login);
        Task<bool> CreateGym(CreateGymInput input);
        Task<GymTokenDto> LogIn(LoginInput login);
    }
}