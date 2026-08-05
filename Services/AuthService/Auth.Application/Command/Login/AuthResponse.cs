using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Command.Login
{
    public record AuthResponse(string AccessToken, Guid UserId, string Email, string Role);
}
