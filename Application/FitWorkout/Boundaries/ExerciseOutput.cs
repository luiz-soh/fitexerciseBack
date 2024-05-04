using Domain.DTOs.FitWorkout;

namespace Application.FitWorkout.Boundaries
{
    public class ExerciseOutput
    {
        public ExerciseOutput(GetExercisesDto exercises)
        {
            Id = exercises.Id;
            Name = exercises.Name;
            S3Path = exercises.S3Path;
            ImgPath = exercises.ImgPath;
        }

        public ExerciseOutput()
        {
            Id = 0;
            Name = string.Empty;
            S3Path = string.Empty;
            ImgPath = string.Empty;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string S3Path { get; set; }
        public string ImgPath { get; set; }
    }
}
