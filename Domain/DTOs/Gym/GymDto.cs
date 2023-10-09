using Domain.Entities.Gym;

namespace Domain.DTOs.Gym
{
    public class GymDto
    {
        public GymDto()
        {
            GymId = 0;
            GymName = string.Empty;
            PlanId = 0;
            PlanDescription = string.Empty;
        }

        public GymDto(GymEntity input)
        {
            GymId = input.GymId;
            GymName = input.GymName;
            PlanId = input.PlanId;
            PlanDescription = string.Empty;
        }

        public int GymId { get; set; }
        public string GymName { get; set; }
        public int PlanId { get; set; }
        public string PlanDescription { get; set; }
    }
}
