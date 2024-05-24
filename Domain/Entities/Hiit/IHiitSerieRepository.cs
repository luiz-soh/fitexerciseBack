using Domain.DTOs.Hiit;

namespace Domain.Entities.Hiit
{
    public interface IHiitSerieRepository
    {
        Task<List<HiitSerieDto>> GetHiitSerieByHiitId(int hiitId, int take);
    }
}