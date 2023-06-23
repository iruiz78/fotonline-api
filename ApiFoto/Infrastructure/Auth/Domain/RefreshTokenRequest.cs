namespace ApiFoto.Infrastructure.Auth.Domain
{
    public class RefreshTokenRequest
    {
        public string TokenExpired { get; set; }
        public string TokenRefresh { get; set; }
        public int UserId { get; set; }
    }
}
