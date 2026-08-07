using Infrastructure;
using Infrastructure.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shopping.Application.Command.AddShoppingList;
namespace Shopping.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AddShoppingListCommand).Assembly));

            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("Connection")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            return services;
        }
    }
}
