using ApiFoto.Domain.Settings.Modules;
using ApiFoto.Domain.Settings.Rols;
using ApiFoto.Infrastructure.Auth.Domain;

namespace ApiFoto.Infrastructure.Extensions
{
    public static class AppSettingsExtensions
    {
        public static void AddAppSettingsExtensions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));
            services.Configure<ModulesSettings>(configuration.GetSection(nameof(ModulesSettings)));
            services.Configure<RolsSettings>(configuration.GetSection(nameof(RolsSettings)));
        }
    }
}
