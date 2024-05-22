using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.GroupWorkout
{
    [Table("group_workout")]
    public class GroupWorkoutEntity
    {

        public GroupWorkoutEntity(int userId, string name)
        {
            GroupId = 0;
            GroupName = name;
            UserId = userId;
        }
        public GroupWorkoutEntity()
        {
            GroupId = 0;
            GroupName = string.Empty;
            UserId = 0;
        }


        [Column("group_workout_id")]
        [Key]
        public int GroupId { get; set; }

        [Column("group_name")]
        public string GroupName { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }
    }
}