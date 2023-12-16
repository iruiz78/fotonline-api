namespace ApiFoto.Domain.AwsS3
{
    public class AwsS3Entit
    {
        //public class S3Object
        //{
        //    public string Name { get; set; } = null!;
        //    public MemoryStream InputStream { get; set; } = null!;
        //    public string BucketName { get; set; } = null!;
        //}

        //public class S3ResponseDto
        //{
        //    public int StatusCode { get; set; }
        //    public string Message { get; set; }
        //}

        //public class AwsCredentials
        //{
        //    public string AccessKey { get; set; } = "";
        //    public string SecretKey { get; set; } = "";
        //}

        //public class Constants
        //{
        //    public static string AccessKey = "AccessKey";
        //    public static string SecretKey = "SecretKey";
        //}

        public class S3ObjectDto
        {
            public string? Name { get; set; }
            public string? PresignedUrl { get; set; }
        }
    }
}
