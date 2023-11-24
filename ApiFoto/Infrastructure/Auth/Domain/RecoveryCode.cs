using ApiFoto.Domain;

namespace ApiFoto.Infrastructure.Auth.Domain
{
    public class RecoveryCode : Audit
    {
        public int Id { get; set; }
        public string Mail { get; set; }
        public string Code { get; set; }
        public DateTime DateExpiration { get; set; }
    }
}
