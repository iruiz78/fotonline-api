using Microsoft.AspNetCore.Mvc;
using static ApiFoto.Domain.AwsS3.AwsS3Entit;

namespace ApiFoto.Services.AwsS3
{
    public interface IAwsS3Service
    {
        //Task<S3ResponseDto> UploadFileAsync(S3Object obj, AwsCredentials awsCredentialsValues);
        Task<string> UploadPhotoAsync(Stream fileStream, string fileName);
        Task<string> UploadFileAsync(IFormFile file, string? prefix);
    }
}