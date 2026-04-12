using FocusFlow.API.Contracts.Tasks;
using FocusFlow.Application.Features.Tasks.Commands.CreateTask;
using FocusFlow.Application.Features.Tasks.Commands.DeleteTask;
using FocusFlow.Application.Features.Tasks.Commands.UpdateTask;
using FocusFlow.Application.Features.Tasks.Queries.GetTasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FocusFlow.API.Controllers;

[ApiController]
[Authorize]
[Route("api/tasks")]
public sealed class TasksController : ControllerBase
{
    private readonly ISender _sender;
    public TasksController(ISender sender) => _sender = sender;
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken) => Ok(await _sender.Send(new GetTasksQuery(), cancellationToken));
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTaskRequest request, CancellationToken cancellationToken) => Ok(await _sender.Send(new CreateTaskCommand(request.Title, request.Priority), cancellationToken));
    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTaskRequest request, CancellationToken cancellationToken) => Ok(await _sender.Send(new UpdateTaskCommand(id, request.IsCompleted), cancellationToken));
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken) { await _sender.Send(new DeleteTaskCommand(id), cancellationToken); return NoContent(); }
}
