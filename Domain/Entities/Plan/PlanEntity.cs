using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Plan
{
    [Table("plan")]
    public class PlanEntity
    {
        public PlanEntity()
        {
            PlanId = 0;
            PlanDescription = string.Empty;
            PlanValue = 0.00;
            MaxUsers = 0;
        }

        //public PlanEntity(PlanDTO input)
        //{
        //    PlanId = input.PlanId;
        //    PlanDescription = input.PlanDescription;
        //    MaxUsers = input.MaxUsers;
        //    PlanValue = input.PlanValue;
        //}

        [Column("plan_id")]
        [Key]
        public int PlanId { get; set; }

        [Column("plan_description")]
        public string PlanDescription { get; set; }

        [Column("plan_max_users")]
        public int MaxUsers { get; set; }

        [Column("plan_value")]
        public double PlanValue { get; set; }

    }
}
