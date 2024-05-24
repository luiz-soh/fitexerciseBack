using Domain.Configuration;
using Domain.DTOs.Hiit;
using Domain.Entities.Hiit;
using Infra.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infra.Repository.Hiit
{
    public class HiitSerieRepository : IHiitSerieRepository
    {
        private readonly DbContextOptions<ContextBase> _optionsBuilder;
        private readonly IOptions<Secrets> _secrets;

        public HiitSerieRepository(IOptions<Secrets> secrets)
        {
            _secrets = secrets;
            _optionsBuilder = new DbContextOptions<ContextBase>();
        }

        public async Task<List<HiitSerieDto>> GetHiitSerieByHiitId(int hiitId, int take)
        {
            var response = new List<HiitSerieDto>();
            using var context = new ContextBase(_optionsBuilder, _secrets);
            var serie = await context.HiitSerie.Where(x => x.HiitId == hiitId).OrderBy(x => x.SerieId).Take(take).ToListAsync();

            response.AddRange(serie.Select(x => new HiitSerieDto(x)));
            return response;
        }
    }
}