using Auth.Application.Interfaces;
using Auth.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure
{
    public class AuthUnitOfWork : IAuthUnitOfWork
    {
        private readonly AuthDbContext _context;

        public AuthUnitOfWork(AuthDbContext context)
        {
            _context = context;
        }

        public DbSet<UserCredential> Users => _context.Users;
        public DbSet<RefreshToken> RefreshTokens => _context.RefreshTokens;

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => _context.SaveChangesAsync(cancellationToken);
    }
}
