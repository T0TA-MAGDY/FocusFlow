using FocusFlow.Application.Common.Interfaces;
using FocusFlow.Application.DTOs.Tasks;
using FocusFlow.Domain.Interfaces;
using MediatR;

namespace FocusFlow.Application.Features.Tasks.Queries.GetTasks;

public sealed class GetTasksQueryHandler : IRequestHandler<GetTasksQuery, IReadOnlyList<TaskDto>>
{
    private readonly ITaskRepository _tasks; private readonly ICurrentUserService _user;
    public GetTasksQueryHandler(ITaskRepository tasks, ICurrentUserService user) { _tasks = tasks; _user = user; }
    public async Task<IReadOnlyList<TaskDto>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
        => (await _tasks.GetByUserIdAsync(_user.UserId, cancellationToken)).Select(t => new TaskDto(t.Id, t.Title, t.Priority.ToString(), t.IsCompleted, t.CreatedAt)).ToList();
}

