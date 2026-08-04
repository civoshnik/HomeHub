using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Auth.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAuthInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("Connection")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
