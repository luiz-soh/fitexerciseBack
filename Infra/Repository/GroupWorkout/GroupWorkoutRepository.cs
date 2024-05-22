using Domain.Configuration;
using Domain.DTOs.GrouptWorkout;
using Domain.Entities.GroupWorkout;
using Infra.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infra.Repository.GroupWorkout
{
        public class GroupWorkoutRepository : IGroupWorkoutRepository
    {
        private readonly DbContextOptions<ContextBase> _optionsBuilder;
        private readonly IOptions<Secrets> _secrets;

        public GroupWorkoutRepository(IOptions<Secrets> secrets)
        {
            _secrets = secrets;
            _optionsBuilder = new DbContextOptions<ContextBase>();
        }

        public async Task CreateGroupWorkout(int userId, string name)
        {
            using var context = new ContextBase(_optionsBuilder, _secrets);
            var entity = new GroupWorkoutEntity(userId, name);
            context.GroupWorkout.Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task<List<GroupWorkoutDto>> ListGroupWorkout(int userId)
        {
            using var context = new ContextBase(_optionsBuilder, _secrets);
            var groups = await context.GroupWorkout.Where(x => x.UserId == userId).Select(e =>
                new GroupWorkoutDto(e)
            ).AsNoTracking().ToListAsync();

            return groups;

        }

        public async Task<GroupWorkoutDto> GetGroupById(int id)
        {
            using var context = new ContextBase(_optionsBuilder, _secrets);
            var group = await context.GroupWorkout.Where(x => x.GroupId == id).Select(e =>
                new GroupWorkoutDto(e)
            ).AsNoTracking().FirstOrDefaultAsync();

            if (group != null)
                return group;
            else
            {
                return new GroupWorkoutDto();
            }
        }

        public async Task UpdateGroupWorkout(int id, string name)
        {
            using var context = new ContextBase(_optionsBuilder, _secrets);
            var entity = await context.GroupWorkout.Where(x => x.GroupId == id).FirstOrDefaultAsync();

            if (entity != null)
            {
                entity.GroupName = name;
                context.GroupWorkout.Update(entity);
                await context.SaveChangesAsync();
            }

        }

        public async Task DeleteGroupWorkout(int id, int userId)
        {
            using var context = new ContextBase(_optionsBuilder, _secrets);
            var entity = await context.GroupWorkout.Where(x => x.GroupId == id && x.UserId == userId).FirstOrDefaultAsync();

            if (entity != null)
            {
                context.GroupWorkout.Remove(entity);
                await context.SaveChangesAsync();
            }
        }
    }
}