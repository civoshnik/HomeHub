using Auth.Application.Command.Login;
using Auth.Application.Command.Register;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterCommand command)
    {
        var userId = await _mediator.Send(command);
        return Ok(new { userId });
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }
    [Authorize]
    [HttpGet("me")]
    public IActionResult Me()
    {
        return Ok(new
        {
            message = "Вы авторизованы",
            userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
        });
    }
}