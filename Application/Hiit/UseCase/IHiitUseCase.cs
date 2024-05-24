using Domain.DTOs.Hiit;

namespace Application.Hiit.UseCase
{
    public interface IHiitUseCase
    {
        Task<List<HiitDto>> GetHiitByCategoryId(int hiitGrouId);
        Task<List<HiitSerieDto>> GetHiitSeriesByHiitId(int hiitId, int take);
    }
}