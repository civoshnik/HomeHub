using Auth.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure;

public class AuthDbContext : DbContext
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) {}
    public DbSet<UserCredential> Users { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<UserCredential>(entity =>
        {
            entity.ToTable("users");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(200);

            entity.HasIndex(x => x.Email)
                .IsUnique();

            entity.Property(x => x.PasswordHash)
                .IsRequired();

            entity.Property(x => x.Role)
                .IsRequired()
                .HasMaxLength(50);
        });

        builder.Entity<RefreshToken>(entity =>
        {
            entity.ToTable("refresh_tokens");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.UserId)
                .IsRequired();

            entity.HasIndex(x => x.UserId);

            entity.Property(x => x.Token)
                .IsRequired()
                .HasMaxLength(500);

            entity.HasIndex(x => x.Token)
                .IsUnique();

            entity.Property(x => x.ExpiresAt)
                .IsRequired();

            entity.HasOne<UserCredential>()
                  .WithMany()
                  .HasForeignKey(x => x.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}