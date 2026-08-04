using MediatR;

namespace Auth.Application.Command.Register
{
    public record RegisterCommand(string Email, string Password) : IRequest<Guid>
    {
    }
}
