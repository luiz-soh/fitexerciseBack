using Domain.Enums;

namespace Domain.DTOs.S3
{
    public class UploadDto
    {
        public UploadDto(string videoPath, string imgPath, string workoutName, string fileName, ExerciseTypeEnum type)
        {
            VideoPath = videoPath;
            ImgPath = imgPath;
            WorkoutName = workoutName;
            FileName = fileName;
            Type = type;
        }

        public UploadDto()
        {
            VideoPath = string.Empty;
            WorkoutName = string.Empty;
            FileName = string.Empty;
            ImgPath = string.Empty;
            Type = ExerciseTypeEnum.Peito;
        }

        public string VideoPath { get; set; }
        public string ImgPath { get; set; }
        public string WorkoutName { get; set; }
        public string FileName { get; set; }
        public ExerciseTypeEnum Type { get; set; }

    }
}