namespace ApiFoto.Infrastructure.Auth.Domain
{
    public class AuthRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public int Provider { get; set; }
        public string? FullName { get; set; }
    }
}
