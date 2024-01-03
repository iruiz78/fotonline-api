using ApiFoto.Infrastructure.Auth.Domain;
using Loggin;
using Microsoft.Extensions.Options;

namespace ApiFoto.Infrastructure.Extensions.Loggin
{
    public static class LogginExtensions
    {
        public static void AddLogginExtensions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<Log>(configuration.GetSection(nameof(Log)));
            services.AddSingleton(x => (ILog)x.GetRequiredService<IOptions<Log>>().Value);
        }
    }
}
