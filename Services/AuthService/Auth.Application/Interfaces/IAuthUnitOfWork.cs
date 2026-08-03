using Microsoft.EntityFrameworkCore;
using Auth.Domain.Models;
namespace Auth.Application.Interfaces
{
    public interface IAuthUnitOfWork
    {
        public DbSet<UserCredential> Users { get; }
        public DbSet<RefreshToken> RefreshTokens { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
