using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Domain.DTOs.Gym;

namespace Domain.Entities.Gym
{
    [Table("gym")]
    public class GymEntity
    {
        public GymEntity()
        {
            GymId = 0;
            GymName = string.Empty;
            PlanId = 0;
            GymPassword = string.Empty;
            GymLogin = string.Empty;
            GymEmail = string.Empty;
        }

        public GymEntity(CreateGymDto input)
        {
            GymId = 0;
            GymName = input.GymName;
            PlanId = input.PlanId;
            GymPassword = input.Password;
            GymLogin = input.Login;
            GymEmail = input.Email;
        }

        [Column("gym_id")]
        [Key]
        public int GymId { get; set; }

        [Column("gym_name")]
        public string GymName { get; set; }

        [Column("plan_id")]
        public int PlanId { get; set; }

        [Column("gym_password")]
        public string GymPassword { get; set; }

        [Column("gym_login")]
        public string GymLogin { get; set; }

        [Column("gym_email")]
        public string GymEmail { get; set; }
    }
}
