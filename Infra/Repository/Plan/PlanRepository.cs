using Domain.Configuration;
using Domain.DTOs.Plan;
using Domain.Entities.Plan;
using Infra.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infra.Repository.Plan
{
    public class PlanRepository : IPlanRepository
    {
        private readonly DbContextOptions<ContextBase> _optionsBuilder;
        private readonly IOptions<Secrets> _secrets;

        public PlanRepository(IOptions<Secrets> secrets)
        {
            _optionsBuilder = new DbContextOptions<ContextBase>();
            _secrets = secrets;
        }

        public async Task<List<PlanDto>> GetPlans()
        {
            using var context = new ContextBase(_optionsBuilder, _secrets);
            var plans = await context.Plan.Select(x => new PlanDto(x)).AsNoTracking().ToListAsync();

            return plans;
        }
    }
}
