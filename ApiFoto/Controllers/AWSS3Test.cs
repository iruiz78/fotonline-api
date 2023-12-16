using Amazon.S3;
using Amazon.S3.Model;
using ApiFoto.Services.AwsS3;
using Microsoft.AspNetCore.Mvc;
using static ApiFoto.Domain.AwsS3.AwsS3Entit;


namespace ApiFoto.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AWSS3Test : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IAwsS3Service _service;
        private readonly IAmazonS3 _s3Client;
        public AWSS3Test(IConfiguration config, IAwsS3Service service, IAmazonS3 s3Client)
        {
            _service = service;
            _config = config;
            _s3Client = s3Client;
        }

        //[HttpPost(Name = "UploadFile")]
        //public async Task<IActionResult> UploadFile(IFormFile file)
        //{
        //    // Process file
        //    await using var memoryStream = new MemoryStream();
        //    await file.CopyToAsync(memoryStream);

        //    var fileExt = Path.GetExtension(file.FileName);
        //    string guid = Guid.NewGuid().ToString();
        //    var docName = $"{file.FileName}{fileExt}";
        //    // call server

        //    var s3Obj = new S3Object()
        //    {
        //        BucketName = "fotonline-prueba-s3",
        //        InputStream = memoryStream,
        //        Name = docName
        //    };

        //    var cred = new AwsCredentials()
        //    {
        //        AccessKey = _config["AwsConfiguration:AWSAccessKey"],
        //        SecretKey = _config["AwsConfiguration:AWSSecretKey"]
        //    };

        //    var result = await _service.UploadFileAsync(s3Obj, cred);
        //    return Ok(result);

        //}

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFileAsync(IFormFile file, string bucketName, string? prefix)
        {
            var bucketExists = await _s3Client.DoesS3BucketExistAsync(bucketName);
            if (!bucketExists) return NotFound($"Bucket {bucketName} does not exist.");
            var request = new PutObjectRequest()
            {
                BucketName = bucketName,
                Key = string.IsNullOrEmpty(prefix) ? file.FileName : $"{prefix?.TrimEnd('/')}/{file.FileName}",
                InputStream = file.OpenReadStream()
            };
            request.Metadata.Add("Content-Type", file.ContentType);
            await _s3Client.PutObjectAsync(request);
            return Ok($"File {prefix}/{file.FileName} uploaded to S3 successfully!");
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllFilesAsync(string bucketName, string? prefix)
        {
            var bucketExists = await _s3Client.DoesS3BucketExistAsync(bucketName);
            if (!bucketExists) return NotFound($"Bucket {bucketName} does not exist.");
            var request = new ListObjectsV2Request()
            {
                BucketName = bucketName,
                Prefix = prefix
            };
            var result = await _s3Client.ListObjectsV2Async(request);
            var s3Objects = result.S3Objects.Select(s =>
            {
                var urlRequest = new GetPreSignedUrlRequest()
                {
                    BucketName = bucketName,
                    Key = s.Key,
                    Expires = DateTime.UtcNow.AddMinutes(1)
                };
                return new S3ObjectDto()
                {
                    Name = s.Key.ToString(),
                    PresignedUrl = _s3Client.GetPreSignedURL(urlRequest),
                };
            });
            return Ok(s3Objects);
        }

        [HttpGet("get-by-key")]
        public async Task<IActionResult> GetFileByKeyAsync(string bucketName, string key)
        {
            var bucketExists = await _s3Client.DoesS3BucketExistAsync(bucketName);
            if (!bucketExists) return NotFound($"Bucket {bucketName} does not exist.");
            var s3Object = await _s3Client.GetObjectAsync(bucketName, key);
            return File(s3Object.ResponseStream, s3Object.Headers.ContentType);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteFileAsync(string bucketName, string key)
        {
            var bucketExists = await _s3Client.DoesS3BucketExistAsync(bucketName);
            if (!bucketExists) return NotFound($"Bucket {bucketName} does not exist");
            await _s3Client.DeleteObjectAsync(bucketName, key);
            return NoContent();
        }

        [HttpPost("UploadPhotos")]
        public async Task<IActionResult> UploadPhotos(IFormFile file, string? prefix)
            => Ok(_service.UploadFileAsync(file, prefix));
        
    }
}
