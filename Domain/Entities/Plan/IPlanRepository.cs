using Domain.DTOs.Plan;

namespace Domain.Entities.Plan
{
    public interface IPlanRepository
    {
        Task<List<PlanDto>> GetPlans();
    }
}
