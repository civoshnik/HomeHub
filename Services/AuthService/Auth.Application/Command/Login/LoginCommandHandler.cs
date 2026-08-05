using Auth.Application.Interfaces;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Auth.Application.Command.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtService _jwtService;

    public LoginCommandHandler(IUnitOfWork unitOfWork, IJwtService jwtService)
    {
        _unitOfWork = unitOfWork;
        _jwtService = jwtService;
    }

    public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken)
            ?? throw new Exception("Неверный Email или пароль");

        var isValidPassword = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

        if (!isValidPassword)
            throw new Exception("Неверный Email или пароль");

        var accessToken = _jwtService.GenerateToken(user.Id, user.Email, user.Role.ToString());

        user.LastAuthorizedAt = DateTimeOffset.UtcNow;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new AuthResponse(accessToken, user.Id, user.Email, user.Role.ToString());
    }
}