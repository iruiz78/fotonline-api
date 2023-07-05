using System.ComponentModel.DataAnnotations.Schema;

namespace ApiFoto.Domain.User
{
    [Table("Users")]
    public class User : Audit
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
    }
}
