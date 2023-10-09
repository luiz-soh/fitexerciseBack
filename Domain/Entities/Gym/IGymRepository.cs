using Domain.DTOs.Gym;

namespace Domain.Entities.Gym
{
    public interface IGymRepository
    {
        Task CreateGym(CreateGymDto input);
        Task<GymDto> LoginGym(string login, string password);
        Task<bool> GymAlreadyExists(string login);
    }
}
