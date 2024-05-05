using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Application.S3.Boundaries
{
    public class UploadInput
    {
        public UploadInput()
        {
            ExerciseName = string.Empty;
        }
        public UploadInput(string exerciseName, IFormFile file, IFormFile img, int? gymId)
        {
            ExerciseName = exerciseName;
            File = file;
            Img = img;
            GymId = gymId;
        }

        [Required]
        public string ExerciseName { get; set; }

        [Required]
        public IFormFile? File { get; set; }

        [Required]
        public IFormFile? Img { get; set; }

        public int? GymId { get; set; }
    }
}
