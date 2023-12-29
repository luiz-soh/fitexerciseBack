using Domain.Configuration;
using Domain.DTOs.Authentication;
using Domain.DTOs.User;
using Domain.Entities.User;
using Infra.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infra.Repository.User
{
    public class UserRepository : IUserRepository
    {

        private readonly DbContextOptions<ContextBase> _optionsBuilder;
        private readonly IOptions<Secrets> _secrets;

        public UserRepository(IOptions<Secrets> secrets)
        {
            _optionsBuilder = new DbContextOptions<ContextBase>();
            _secrets = secrets;
        }

        public async Task<UserDto> SignIn(SignInDto input)
        {
            using var contexto = new ContextBase(_optionsBuilder, _secrets);
            var user = await contexto.FitUser.Where(x => x.Username == input.Username &&
            x.Password == input.Password).FirstOrDefaultAsync();

            if (user != null)
                return new UserDto(user);
            else
                return new UserDto();
        }

        public async Task SignUp(SignUpDto input)
        {

            var user = new FitUser(input);
            using var contexto = new ContextBase(_optionsBuilder, _secrets);
            await contexto.FitUser.AddAsync(user);
            await contexto.SaveChangesAsync();
        }

        public async Task UpdateRefreshToken(int userId, string refreshToken)
        {
            using var contexto = new ContextBase(_optionsBuilder, _secrets);
            var user = await contexto.FitUser.Where(x => x.UserId == userId).FirstOrDefaultAsync();
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.LastLogin = DateTime.UtcNow;
                contexto.FitUser.Update(user);
                await contexto.SaveChangesAsync();
            }
        }

        public async Task<UserDto> GetRefreshToken(string refreshToken, int userId)
        {
            using var context = new ContextBase(_optionsBuilder, _secrets);
            var user = await context.FitUser.Where(x => x.RefreshToken == refreshToken && x.UserId == userId).FirstOrDefaultAsync();

            if (user != null)
                return new UserDto(user);
            else
                return new UserDto();
        }

        public async Task<bool> UserAlreadyExists(string username, string email)
        {
            using var context = new ContextBase(_optionsBuilder, _secrets);
            return await context.FitUser.Where(u => u.Username == username || u.UserEmail == email).AnyAsync();
        }

        public async Task<bool> DeleteUser(int userId)
        {
            using var context = new ContextBase(_optionsBuilder, _secrets);
            var user = await context.FitUser.Where(x => x.UserId == userId).FirstOrDefaultAsync();

            if (user != null)
            {
                context.FitUser.Remove(user);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task AddUserEmail(string email, int userId)
        {
            using var context = new ContextBase(_optionsBuilder, _secrets);
            var user = await context.FitUser.Where(x => x.UserId == userId).FirstOrDefaultAsync();

            if (user != null)
            {
                user.UserEmail = email;
                context.FitUser.Update(user);
                await context.SaveChangesAsync();
            }
        }

        public async Task<UserDto> GetUserData(int userId)
        {
            using var context = new ContextBase(_optionsBuilder, _secrets);
            var user = await context.FitUser.Where(x => x.UserId == userId).FirstOrDefaultAsync();

            if (user != null)
                return new UserDto(user);
            else
                return new UserDto();
        }

        public async Task<List<UserDto>> GetUsersByGymId(int gymId)
        {
            using var context = new ContextBase(_optionsBuilder, _secrets);

            return await context.FitUser.Where(x => x.GymId == gymId).Select(u => new UserDto(u)).AsNoTracking().ToListAsync();
        }
    }
}
