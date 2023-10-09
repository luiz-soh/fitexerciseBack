using Domain.Entities.Plan;

namespace Domain.DTOs.Plan
{
    public class PlanDto
    {
        public PlanDto()
        {
            PlanId = 0;
            PlanDescription = string.Empty;
            PlanValue = 0.00;
            MaxUsers = 0;
        }

        public PlanDto(PlanEntity input)
        {
            PlanId = input.PlanId;
            PlanDescription = input.PlanDescription;
            MaxUsers = input.MaxUsers;
            PlanValue = input.PlanValue;
        }

        public int PlanId { get; set; }

        public string PlanDescription { get; set; }

        public int MaxUsers { get; set; }

        public double PlanValue { get; set; }
    }
}
