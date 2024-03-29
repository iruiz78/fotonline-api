﻿using ApiFoto.Domain.Settings.Modules;
using ApiFoto.Domain.User;
using AutoMapper;

namespace ApiFoto.Infrastructure.Mapper
{
    public static class Mapping
    {
        public static void InitializeMapper(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserRequest, User>().IgnoreAllSourcePropertiesWithAnInaccessibleSetter();
                cfg.CreateMap<ModuleRequest, Module>().IgnoreAllSourcePropertiesWithAnInaccessibleSetter();
                cfg.CreateMap<UserRequest, UserRequestUpdate>().IgnoreAllSourcePropertiesWithAnInaccessibleSetter();
            });
            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
