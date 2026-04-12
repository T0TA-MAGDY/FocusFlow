using MediatR;

namespace FocusFlow.Application.Features.Tasks.Commands.DeleteTask;

public sealed record DeleteTaskCommand(Guid TaskId) : IRequest<Unit>;
