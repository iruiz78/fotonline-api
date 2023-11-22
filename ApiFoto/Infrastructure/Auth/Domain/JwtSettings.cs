namespace ApiFoto.Infrastructure.Auth.Domain
{
    public class JwtSettings
    {
        public string Key { get; set; }
        public int Expire { get; set; }
        public int ExpireRefresh { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
