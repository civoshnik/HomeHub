using Auth.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Auth.API;

public static class MigrationExtensions
{
    public static void ApplyAuthMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AuthDbContext>();
        db.Database.Migrate();
    }
}