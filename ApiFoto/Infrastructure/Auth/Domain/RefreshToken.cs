using ApiFoto.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiFoto.Infrastructure.Auth.Domain
{
    [Table("RefreshTokens")]
    public class RefreshToken : Audit
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
        public string TokenRefresh { get; set; }
        public DateTime ExpiratedDate { get; set; }

    }
}
