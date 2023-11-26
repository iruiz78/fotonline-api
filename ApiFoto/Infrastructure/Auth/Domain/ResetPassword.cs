namespace ApiFoto.Infrastructure.Auth.Domain
{
    public class SendCodeRequest
    {
        public string Email { get; set; }
    }
    public class ValidateCodeRequest
    {
        public string Email { get; set; }
        public string Code { get; set; }
    }

    public class ResetPasswordRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
