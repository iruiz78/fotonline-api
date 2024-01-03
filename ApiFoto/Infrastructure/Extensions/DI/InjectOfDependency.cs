using ApiFoto.Domain.Event;
using ApiFoto.Domain.User;
using ApiFoto.Helpers;
using ApiFoto.Infrastructure.Extensions.AppSettings;
using ApiFoto.Infrastructure.Extensions.Loggin;
using ApiFoto.Infrastructure.ULogged;
using ApiFoto.Repository.Authentication;
using ApiFoto.Repository.Events;
using ApiFoto.Repository.Generic;
using ApiFoto.Repository.Users;
using ApiFoto.Services.Authentication;
using ApiFoto.Services.Events;
using ApiFoto.Services.Users;

namespace ApiFoto.Infrastructure.Extensions.DI
{
    public static class InjectOfDependency
    {
        public static void InitializeInjections(this IServiceCollection services, IConfiguration configuration)
        {
            // Extensions => Esto en la proxima vuela de aca
            services.AddLogginExtensions(configuration);
            services.AddAppSettingsExtensions(configuration);


            // Utils
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IUserLogged, UserLogged>();
            services.AddTransient<MailService>();



            // Repositories
            services.AddTransient<IGenericRepository<Auth.Domain.Auth>, AuthRepository>();
            services.AddTransient<IGenericRepository<User>, UserRepository>();
            services.AddTransient<IGenericRepository<Event>, EventRepository>();


            // Services
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IEventService, EventService>();

        }

    }
}
