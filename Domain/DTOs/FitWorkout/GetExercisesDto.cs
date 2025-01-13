using Domain.Enums;

namespace Domain.DTOs.FitWorkout
{
    public class GetExercisesDto
    {
        public GetExercisesDto(int id, string name, string s3Path, string imgPath, ExerciseTypeEnum type)
        {
            Id = id;
            Name = name;
            S3Path = s3Path;
            ImgPath = imgPath;
            Type = type;
        }

        public GetExercisesDto()
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
