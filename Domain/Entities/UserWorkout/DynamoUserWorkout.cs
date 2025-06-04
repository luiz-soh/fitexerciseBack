using Amazon.DynamoDBv2.DataModel;

namespace Domain.Entities.UserWorkout
{
    [DynamoDBTable("userWorkouts")]
    public class DynamoUserWorkout
    {
        public DynamoUserWorkout(int groupId, string workoutPlan)
        {
            GroupId = groupId;
            WorkoutPlan = workoutPlan;
        }
        public DynamoUserWorkout()
        {
            GroupId = 0;
            WorkoutPlan = string.Empty;
        }

        [DynamoDBHashKey("groupId")]
        public int GroupId { get; set; }

        [DynamoDBProperty("workoutPlan")]
        public string WorkoutPlan { get; set; }
    }
}