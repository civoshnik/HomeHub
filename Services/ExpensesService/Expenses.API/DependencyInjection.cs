using Expenses.Application.Interfaces;
using Infrastructure;
using Infrastructure.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace Expenses.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(SaveBudgetCommand).Assembly));

            services.AddDbContext<AppDbContext>(options =>options.UseNpgsql(configuration.GetConnectionString("Connection")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddHttpClient<IAiService, AiService>();

            return services;
        }
    }
}
