using Domain.DTOs.Hiit;

namespace Domain.Entities.Hiit
{
    public interface IHiitRepository
    {
        Task<List<HiitDto>> GetHiitByCategoryId(int hiitGroupId);
    }
}