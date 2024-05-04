namespace Domain.DTOs.FitWorkout
{
    public class GetExercisesDto
    {
        public GetExercisesDto(int id, string name, string s3Path, string imgPath)
        {
            Id = id;
            Name = name;
            S3Path = s3Path;
            ImgPath = imgPath;
        }

        public GetExercisesDto()
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
