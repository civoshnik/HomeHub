using Auth.Application.Command.Register;
using Auth.Application.Interfaces;
using Auth.Infrastructure.Security;
using Infrastructure;
using Infrastructure.Security;
using Microsoft.EntityFrameworkCore;

namespace Auth.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAuthInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("Connection")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RegisterCommand).Assembly));

            services.AddScoped<IJwtService, JwtService>();

            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            return services;
        }
    }
}
