using ApiFoto.Domain.User;

namespace ApiFoto.Infrastructure.ULogged
{
    public interface IUserLogged
    {
        public UserResponse User { get; }
    }
}