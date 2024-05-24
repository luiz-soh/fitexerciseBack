using Domain.Configuration;
using Domain.DTOs.Hiit;
using Domain.Entities.Hiit;
using Infra.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infra.Repository.Hiit
{
    public class HiitRepository : IHiitRepository
    {
        private readonly DbContextOptions<ContextBase> _optionsBuilder;
        private readonly IOptions<Secrets> _secrets;

        public HiitRepository(IOptions<Secrets> secrets)
        {
            _secrets = secrets;
            _optionsBuilder = new DbContextOptions<ContextBase>();
        }

        public async Task<List<HiitDto>> GetHiitByCategoryId(int hiitCategoryId)
        {
            var response = new List<HiitDto>();
            using var context = new ContextBase(_optionsBuilder, _secrets);
            var hiit = await context.Hiit.Where(x => x.HiitCategoryId == hiitCategoryId).AsNoTracking().OrderBy(x => x.HiitPosition).ToListAsync();

            response.AddRange(hiit.Select(x => new HiitDto(x)));
            return response;
        }
    }
}