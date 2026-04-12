using FocusFlow.API.Contracts.Auth;
using FocusFlow.Application.Features.Auth.Commands.Login;
using FocusFlow.Application.Features.Auth.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FocusFlow.API.Controllers;

[ApiController]
[Route("api/auth")]
public sealed class AuthController : ControllerBase
{
    private readonly ISender _sender;
    public AuthController(ISender sender) => _sender = sender;
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
        => Ok(await _sender.Send(new RegisterCommand(request.Username, request.Email, request.Password), cancellationToken));
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
        => Ok(await _sender.Send(new LoginCommand(request.Email, request.Password), cancellationToken));
}
