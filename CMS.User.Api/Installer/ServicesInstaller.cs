using CMS.User.Api.Builders.Response;
using CMS.User.Api.Mappers;
using CMS.User.Api.Repositories;
using CMS.User.Api.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CMS.User.Api.Installer
{
    public static class ServicesInstaller
    {
        public static IServiceCollection AddUserServices(this IServiceCollection services)
        {
            services.AddSingleton<IUserResponseBuilder, UserResponseBuilder>();
            services.AddSingleton<IUserCoreMapper, UserCoreMapper>();
            services.AddSingleton<IDbUserMapper, DbUserMapper>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserGateway, UserGateway>();
            return services;
        }
    }
}
