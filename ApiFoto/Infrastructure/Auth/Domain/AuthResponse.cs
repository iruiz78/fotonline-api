namespace ApiFoto.Infrastructure.Auth.Domain
{
    public class AuthResponse
    {
        public string Token { get; set; }
        public string TokenRefresh { get; set; }
        public DateTime ExpiredDate { get; set; }
        public string UserName { get; set; }
    }
}
