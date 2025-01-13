using Domain.DTOs.FitWorkout;
using Domain.Enums;

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
            Type = exercises.Type;
        }

        public ExerciseOutput()
        {
            Id = 0;
            Name = string.Empty;
            S3Path = string.Empty;
            ImgPath = string.Empty;
            Type = ExerciseTypeEnum.Peito;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string S3Path { get; set; }
        public string ImgPath { get; set; }
        public ExerciseTypeEnum Type { get; set; }
    }
}
