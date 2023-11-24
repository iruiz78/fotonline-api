using ApiFoto.Domain.User;
using ApiFoto.Helpers;
using ApiFoto.Infrastructure.Auth.Domain;
using ApiFoto.Infrastructure.Extensions;
using ApiFoto.Infrastructure.ULogged;
using ApiFoto.Repository.Authentication;
using ApiFoto.Repository.Generic;
using ApiFoto.Repository.Users;
using ApiFoto.Services.Authentication;
using ApiFoto.Services.Users;

namespace ApiFoto.Infrastructure.IoC
{
    public static class DI
    {
        public static void InitializeInjections(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IGenericRepository<Auth.Domain.Auth>, AuthRepository>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IGenericRepository<User>, UserRepository>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IGenericRepository<User>, UserRepository>();
            services.AddTransient<IUserLogged, UserLogged>();
            services.AddAppSettingsExtensions(configuration);
            services.AddLogginExtensions(configuration);
            services.AddTransient<MailService>();
        }

    }
}
