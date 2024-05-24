using Domain.DTOs.Hiit;
using Domain.Entities.Hiit;

namespace Application.Hiit.UseCase
{
    public class HiitUseCase : IHiitUseCase
    {
        private readonly IHiitRepository _hiitRepository;
        private readonly IHiitSerieRepository _hiitSerieRepository;
        public HiitUseCase(IHiitRepository hiitRepository, IHiitSerieRepository hiitSerieRepository)
        {
            _hiitRepository = hiitRepository;
            _hiitSerieRepository = hiitSerieRepository;
        }


        public async Task<List<HiitDto>> GetHiitByCategoryId(int id)
        {
            return await _hiitRepository.GetHiitByCategoryId(id);
        }

        public async Task<List<HiitSerieDto>> GetHiitSeriesByHiitId(int hiitId, int take)
        {
            return await _hiitSerieRepository.GetHiitSerieByHiitId(hiitId, take);
        }
    }
}