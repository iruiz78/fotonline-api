using ApiFoto.Domain.User;
using ApiFoto.Infrastructure.Auth.Domain;
using ApiFoto.Repository.Authentication;
using ApiFoto.Repository.Generic;
using ApiFoto.Repository.Users;
using ApiFoto.Services.Authentication;
using ApiFoto.Services.Users;

namespace ApiFoto.Infrastructure.IoC
{
    public static class DI
    {
        public static void InitializeInjections(this IServiceCollection services)
        {
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IGenericRepository<Auth.Domain.Auth>, AuthRepository>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IGenericRepository<User>, UserRepository>();

            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IGenericRepository<User>, UserRepository>();


        }

    }
}
