using Auth.Application.Interfaces;
using Auth.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Auth.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAuthInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AuthDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("AuthDb")));

            services.AddScoped<IAuthUnitOfWork, AuthUnitOfWork>();

            return services;
        }
    }
}
