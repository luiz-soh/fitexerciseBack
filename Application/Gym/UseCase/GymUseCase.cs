using Application.Gym.Boundaries;
using Application.Token.UseCase;
using Domain.DTOs.Gym;
using Domain.Entities.Gym;
using Domain.Entities.User;

namespace Application.Gym.UseCase
{
    public class GymUseCase(ITokenUseCase tokenUseCase, IGymRepository gymRepository, IUserRepository userRepository) : IGymUseCase
    {
        private readonly IGymRepository _gymRepository = gymRepository;
        private readonly ITokenUseCase _tokenUseCase = tokenUseCase;
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<bool> CanWorkWithUser(int userId, int gymId)
        {
            return await _userRepository.UserBelongsToGym(userId, gymId);
        }

        public async Task<bool> CreateGym(CreateGymInput input)
        {
            var encryptedPassword = _tokenUseCase.EncryptPassword(input.Password);

            var createGym = new CreateGymDto(input.GymName, input.PlanId, encryptedPassword,
             input.Login, input.Email);

            await _gymRepository.CreateGym(createGym);
            return true;
        }

        public async Task<bool> GymExists(string login)
        {
            return await _gymRepository.GymAlreadyExists(login);
        }

        public async Task<GymTokenDto> LogIn(LoginInput login)
        {
            var encryptedPassword = _tokenUseCase.EncryptPassword(login.Password);
            login.Password = encryptedPassword;

            var gym = await _gymRepository.LoginGym(login.Login, login.Password);

            if (gym.GymId > 0)
            {
                return GenerateToken(gym);
            }

            return new GymTokenDto();
        }

        private GymTokenDto GenerateToken(GymDto gym)
        {
            var token = _tokenUseCase.GenerateToken(gym.GymName, "gym", 1, gym.GymId, gym.GymId);

            return new GymTokenDto(gym.GymId, gym.GymName, token);
        }
    }
}