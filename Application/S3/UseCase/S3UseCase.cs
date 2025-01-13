using System.Globalization;
using System.Net;
using System.Text;
using Amazon.S3;
using Amazon.S3.Model;
using Application.S3.Boundaries;
using Domain.DTOs.S3;
using Microsoft.AspNetCore.Http;

namespace Application.S3.UseCase
{
    public class S3UseCase : IS3UseCase
    {
        private readonly IAmazonS3 _s3Client;

        public S3UseCase(IAmazonS3 s3Client)
        {
            _s3Client = s3Client;
        }

        public async Task<UploadDto> UploadFile(UploadInput upload)
        {
            var videoLocation = CreateFileLocation(upload.File!.FileName, upload.ExerciseName, upload.GymId);
            var imgLocation = CreateFileLocation(upload.Img!.FileName, upload.ExerciseName, upload.GymId);

            var videoUpload = await UploadToS3(videoLocation, upload.File);
            var imgUpload = await UploadToS3(imgLocation, upload.Img);

            if (!imgUpload || !videoUpload)
            {
                return new UploadDto();
            }
            return new UploadDto(videoLocation, imgLocation, upload.ExerciseName, upload.File!.FileName, upload.Type);
        }

        public async Task<bool> CopyObject(string path, string newPath)
        {
            var request = new CopyObjectRequest
            {
                SourceBucket = "fitexercise-videos",
                SourceKey = path,
                DestinationBucket = "fitexercise-videos",
                DestinationKey = newPath
            };

            var response = await _s3Client.CopyObjectAsync(request);

            return response.HttpStatusCode == HttpStatusCode.OK;
        }

        public async Task DeleteObject(string source)
        {
            var request = new DeleteObjectRequest
            {
                BucketName = "fitexercise-videos",
                Key = source
            };

            await _s3Client.DeleteObjectAsync(request);
        }

        public string EditFileLocation(string oldPath, string exerciseName)
        {
            var splitedLocation = oldPath.Split("/");
            var exercise = RemoveAccentsWhiteSpace(exerciseName);

            if (splitedLocation.Length == 3)
            {
                splitedLocation[1] = exercise;
            }
            else
            {
                splitedLocation[0] = exercise;
            }

            return string.Join("/", splitedLocation);
        }

        private async Task<bool> UploadToS3(string location, IFormFile file)
        {
            using var stream = file.OpenReadStream();
            var putRequest = new PutObjectRequest
            {
                Key = location,
                BucketName = "fitexercise-videos",
                InputStream = stream,
                AutoCloseStream = true,
                ContentType = file.ContentType
            };
            var response = await _s3Client.PutObjectAsync(putRequest);

            if (response.HttpStatusCode != HttpStatusCode.OK)
            {
                return false;
            }

            return true;
        }

        private static string CreateFileLocation(string fileName, string exerciseName, int? gymId)
        {
            var exercise = RemoveAccentsWhiteSpace(exerciseName);
            var file = RemoveAccentsWhiteSpace(fileName);

            var location = $"{exercise}/{file}";

            if (gymId != null)
            {
                location = $"{gymId}/{exercise}/{file}";
            }

            return location;
        }

        private static string RemoveAccentsWhiteSpace(string text)
        {
            StringBuilder sb = new StringBuilder();
            var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();
            foreach (char letter in arrayText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                    sb.Append(letter);
            }
            var stringReturn = sb.ToString();

            return string.Concat(stringReturn.Where(c => !char.IsWhiteSpace(c)));
        }

    }
}