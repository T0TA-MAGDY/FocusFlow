using FocusFlow.Application.DTOs.Tasks;
using MediatR;

namespace FocusFlow.Application.Features.Tasks.Queries.GetTasks;

public sealed record GetTasksQuery : IRequest<IReadOnlyList<TaskDto>>;
