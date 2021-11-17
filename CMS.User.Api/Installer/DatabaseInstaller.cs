using CMS.User.Api.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CMS.User.Api.Installer
{
    public static class DatabaseInstaller
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            services.AddDbContext<UserDbContext>(opt => opt.UseInMemoryDatabase("User"));

            return services;
        }
    }
}
