namespace ApiFoto.Infrastructure.Auth.Domain
{
    public class SendCodeRequest
    {
        public string Mail { get; set; }
    }
    public class ValidateCodeRequest
    {
        public string Mail { get; set; }
        public string Code { get; set; }
    }

    public class ResetPasswordRequest
    {
        public string Mail { get; set; }
        public string Password { get; set; }
    }
}
