using FocusFlow.Application.DTOs.Tasks;
using MediatR;

namespace FocusFlow.Application.Features.Tasks.Commands.UpdateTask;

public sealed record UpdateTaskCommand(Guid TaskId, bool IsCompleted) : IRequest<TaskDto>;
