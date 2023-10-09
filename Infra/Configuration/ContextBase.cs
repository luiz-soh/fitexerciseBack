using Domain.Configuration;
using Domain.Entities.Gym;
using Domain.Entities.Plan;
using Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infra.Configuration
{
    public class ContextBase : DbContext
    {
        private readonly string _connectionString;

        public ContextBase(DbContextOptions<ContextBase> options,
                   IOptions<Secrets> secrets) : base(options)
        {
            _connectionString = secrets.Value.FitExerciseDB;
        }

        public DbSet<FitUser> FitUser => Set<FitUser>();
        public DbSet<GymEntity> Gyms => Set<GymEntity>();
        public DbSet<PlanEntity> Plan => Set<PlanEntity>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(_connectionString);
                base.OnConfiguring(optionsBuilder);
            }
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
