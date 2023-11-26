using ApiFoto.Domain.Settings.Modules;

namespace ApiFoto.Domain.User
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RolId { get; set; }
        public string Rol { get; set; } // Capaz esta de mas
        public IEnumerable<ModuleResponse> Modules { get; set; }
    }
}
