namespace ApiFoto.Infrastructure.Auth.Domain
{
    public class RefreshTokenResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
        public string TokenRefresh { get; set; }
        public DateTime ExpiratedDate { get; set; }

        public bool IsActive { get
            {
                return this.ExpiratedDate > DateTime.Now;
            }
        }
    }
}
