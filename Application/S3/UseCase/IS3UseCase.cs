using Application.S3.Boundaries;
using Domain.DTOs.S3;

namespace Application.S3.UseCase
{
    public interface IS3UseCase
    {
        Task<UploadDto> UploadFile(UploadInput file);
        Task<bool> CopyObject(string path, string newPath);
        string EditFileLocation(string oldPath, string exerciseName);
        Task DeleteObject(string source);

    }
}