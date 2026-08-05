using MediatR;

namespace Auth.Application.Command.Login;

public record LoginCommand(string Email, string Password) : IRequest<AuthResponse>;