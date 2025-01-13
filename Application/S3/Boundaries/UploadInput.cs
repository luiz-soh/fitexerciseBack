using System.ComponentModel.DataAnnotations;
using Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Application.S3.Boundaries
{
    public class UploadInput
    {
        public UploadInput()
        {
            ExerciseName = string.Empty;
        }
        public UploadInput(string exerciseName, IFormFile file, IFormFile img, int? gymId, ExerciseTypeEnum type)
        {
            ExerciseName = exerciseName;
            File = file;
            Img = img;
            GymId = gymId;
            Type = type;
        }

        [Required]
        public string ExerciseName { get; set; }

        [Required]
        public IFormFile? File { get; set; }

        [Required]
        public IFormFile? Img { get; set; }

        [Required]
        public ExerciseTypeEnum Type { get; set; }

        public int? GymId { get; set; }
    }
}
