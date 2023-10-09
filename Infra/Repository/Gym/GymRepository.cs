using Domain.Configuration;
using Domain.DTOs.Gym;
using Domain.Entities.Gym;
using Infra.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infra.Repository.Gym
{
    public class GymRepository : IGymRepository
    {
        private readonly DbContextOptions<ContextBase> _optionsBuilder;
        private readonly IOptions<Secrets> _secrets;

        public GymRepository(IOptions<Secrets> secrets)
        {
            _optionsBuilder = new DbContextOptions<ContextBase>();
            _secrets = secrets;
        }

        public async Task CreateGym(CreateGymDto input)
        {
            using var context = new ContextBase(_optionsBuilder, _secrets);
            var gym = new GymEntity(input);
            context.Gyms.Add(gym);
            await context.SaveChangesAsync();
        }

        public async Task<bool> GymAlreadyExists(string login)
        {
            using var context = new ContextBase(_optionsBuilder, _secrets);

            return await context.Gyms.AnyAsync(x => x.GymLogin == login);
        }

        public async Task<GymDto> LoginGym(string login, string password)
        {
            using var context = new ContextBase(_optionsBuilder, _secrets);

            var gym = await context.Gyms.Where(x => x.GymPassword == password && x.GymLogin == login).Select(x => new GymDto(x)
            ).FirstOrDefaultAsync();

            if (gym != null)
            {
                return gym;
            }
            else
            {
                return new GymDto();
            }

        }
    }
}
