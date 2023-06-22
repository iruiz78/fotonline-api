using AutoMapper;

namespace ApiFoto.Infrastructure.Mapper
{
    public static class Mapping
    {
        public static void InitializeMapper(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
                
            });
            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
