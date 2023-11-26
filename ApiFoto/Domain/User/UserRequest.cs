using ApiFoto.Domain.Settings.Modules;

namespace ApiFoto.Domain.User
{
    public class UserRequest
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string? Password { get; set; }
        public int RolId { get; set; }
        public List<ModuleRequest> Modules { get; set; }
    }
}
