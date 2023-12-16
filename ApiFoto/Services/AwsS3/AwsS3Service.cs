using Amazon.Runtime;
using Amazon.S3.Transfer;
using Amazon.S3;
using static ApiFoto.Domain.AwsS3.AwsS3Entit;
using Amazon.S3.Model;
using ApiFoto.Infrastructure.Communication;
using ApiFoto.Infrastructure.Communication.Exceptions;
using Microsoft.Extensions.Options;
using ApiFoto.Domain.AwsS3;
using Amazon.S3.Util;

namespace ApiFoto.Services.AwsS3
{
    public class AwsS3Service : IAwsS3Service
    {
        private readonly IAmazonS3 _s3Client;
        private readonly IConfiguration _config;
        private readonly AWSConfiguration _awsConfig;
        public AwsS3Service(IConfiguration config, IAmazonS3 s3Client, IOptions<AWSConfiguration> awsConfig)
        {
            _config = config;
            _s3Client = s3Client;
            _awsConfig = awsConfig.Value;
        }

        public async Task<string> UploadFileAsync(IFormFile file, string? prefix)
        {
            bool bucketExists = await _s3Client.DoesS3BucketExistAsync(_awsConfig.BucketName);
            var bucketExistsV2 = await AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, _awsConfig.BucketName);
            if (!bucketExists) throw new AppException($"Bucket {_awsConfig.BucketName} does not exist.");
            var request = new PutObjectRequest()
            {
                BucketName = _awsConfig.BucketName,
                Key = string.IsNullOrEmpty(prefix) ? file.FileName : $"{prefix?.TrimEnd('/')}/{file.FileName}",
                InputStream = file.OpenReadStream()
            };
            request.Metadata.Add("Content-Type", file.ContentType);
            await _s3Client.PutObjectAsync(request);
            return $"File {prefix}/{file.FileName} uploaded to S3 successfully!";
        }

        public async Task<string> UploadPhotoAsync(Stream fileStream, string fileName)
        {
            //var putRequest = new PutObjectRequest
            //{
            //    BucketName = _bucketName,
            //    Key = fileName,
            //    InputStream = fileStream,
            //    ContentType = "image/jpeg" // O el tipo de contenido correcto
            //};

            //var response = await _s3Client.PutObjectAsync(putRequest);

            //if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            //{
            //    return fileName;
            //}

            //// Manejar el error en consecuencia
            throw new Exception("Error al subir la foto a S3");
        }


        // Con credenciales
        //public async Task<S3ResponseDto> UploadFileAsync(S3Object obj, AwsCredentials awsCredentialsValues)
        //{
        //    Console.WriteLine($"Key: {awsCredentialsValues.AccessKey}, Secret: {awsCredentialsValues.SecretKey}");

        //    var credentials = new BasicAWSCredentials(awsCredentialsValues.AccessKey, awsCredentialsValues.SecretKey);

        //    var config = new AmazonS3Config()
        //    {
        //        RegionEndpoint = Amazon.RegionEndpoint.USEast1
        //    };

        //    var response = new S3ResponseDto();
        //    try
        //    {
        //        var uploadRequest = new TransferUtilityUploadRequest()
        //        {
        //            InputStream = obj.InputStream,
        //            Key = obj.Name,
        //            BucketName = obj.BucketName,
        //            CannedACL = S3CannedACL.NoACL
        //        };

        //        // initialise client
        //        using var client = new AmazonS3Client(credentials, config);

        //        // initialise the transfer/upload tools
        //        var transferUtility = new TransferUtility(client);

        //        // initiate the file upload
        //        await transferUtility.UploadAsync(uploadRequest);

        //        response.StatusCode = 201;
        //        response.Message = $"{obj.Name} has been uploaded sucessfully";
        //    }
        //    catch (AmazonS3Exception s3Ex)
        //    {
        //        response.StatusCode = (int)s3Ex.StatusCode;
        //        response.Message = s3Ex.Message;
        //    }
        //    catch (Exception ex)
        //    {
        //        response.StatusCode = 500;
        //        response.Message = ex.Message;
        //    }

        //    return response;
        //}
    }
}
