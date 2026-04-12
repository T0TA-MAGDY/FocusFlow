using FocusFlow.API.Contracts.Sessions;
using FocusFlow.Application.Features.Sessions.Commands.CreateSession;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FocusFlow.API.Controllers;

[ApiController]
[Authorize]
[Route("api/sessions")]
public sealed class SessionsController : ControllerBase
{
    private readonly ISender _sender;
    public SessionsController(ISender sender) => _sender = sender;
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSessionRequest request, CancellationToken cancellationToken)
        => Ok(await _sender.Send(new CreateSessionCommand(request.TaskId, request.StartTime, request.DurationSeconds), cancellationToken));
}
