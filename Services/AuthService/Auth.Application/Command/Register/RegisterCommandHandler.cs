using Auth.Domain.Enum;
using Auth.Domain.Models;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Auth.Application.Command.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegisterCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Guid> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var exist = await _unitOfWork.Users.AnyAsync(u => u.Email == request.Email, cancellationToken);

            if (exist)
            {
                throw new Exception("Пользователь уже существует");
            }
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new UserCredential
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                PasswordHash = hashedPassword,
                Role = UserRole.User,
                IsEmailConfirmed = false,
                CreatedAt = DateTime.UtcNow,
            };

            await _unitOfWork.Users.AddAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
