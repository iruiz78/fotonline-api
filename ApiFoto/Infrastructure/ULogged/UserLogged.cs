using ApiFoto.Domain.User;
using System.Security.Claims;

namespace ApiFoto.Infrastructure.ULogged
{
    public class UserLogged : IUserLogged
    {

        private readonly IHttpContextAccessor _context;
        public UserLogged(IHttpContextAccessor context)
        {
            _context = context;
        }

        public UserResponse User
        {
            get
            {
                var identity = _context.HttpContext.User.Identity as ClaimsIdentity;
                if (identity == null) return null;
                IEnumerable<Claim> userClaims = identity.Claims;
                return new UserResponse
                {
                    Email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value
                };
            }
            
        }
    }
}
