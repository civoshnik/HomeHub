using Auth.Domain.Enum;
using Auth.Domain.Models;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using User.Domain.Models;

namespace Auth.Application.Command.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;

    public RegisterCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var exist = await _unitOfWork.Users.AnyAsync(u => u.Email == request.Email, cancellationToken);

        if (exist)
            throw new Exception("Пользователь уже существует");

        var userId = Guid.NewGuid();
        var householdId = Guid.NewGuid();

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var user = new UserCredential
        {
            Id = userId,
            Email = request.Email,
            PasswordHash = hashedPassword,
            Role = UserRole.User,
            IsEmailConfirmed = false,
            CreatedAt = DateTime.UtcNow
        };

        var household = new Household
        {
            Id = householdId,
            Name = "Моя семья"
        };

        var profile = new UserProfile
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            HouseholdId = householdId,
            FirsName = "",
            LastName = "",
            PhoneNumber = ""
        };

        var householdMember = new HouseholdMember
        {
            HouseholdId = householdId,
            UserId = userId,
            Role = "Owner"
        };

        await _unitOfWork.Users.AddAsync(user, cancellationToken);
        await _unitOfWork.Households.AddAsync(household, cancellationToken);
        await _unitOfWork.UserProfiles.AddAsync(profile, cancellationToken);
        await _unitOfWork.HouseholdMembers.AddAsync(householdMember, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return userId;
    }
}