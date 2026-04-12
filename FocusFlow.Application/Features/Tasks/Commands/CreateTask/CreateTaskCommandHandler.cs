using FocusFlow.Application.Common.Exceptions;
using FocusFlow.Application.Common.Interfaces;
using FocusFlow.Application.DTOs.Tasks;
using FocusFlow.Domain.Entities;
using FocusFlow.Domain.Enums;
using FocusFlow.Domain.Interfaces;
using MediatR;

namespace FocusFlow.Application.Features.Tasks.Commands.CreateTask;

public sealed class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, TaskDto>
{
    private static readonly string[] Allowed = ["Low", "Medium", "High"];
    private readonly ITaskRepository _tasks; private readonly ICurrentUserService _user; private readonly IUnitOfWork _uow;
    public CreateTaskCommandHandler(ITaskRepository tasks, ICurrentUserService user, IUnitOfWork uow) { _tasks = tasks; _user = user; _uow = uow; }
    public async Task<TaskDto> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var title = request.Title?.Trim() ?? string.Empty;
        if (title.Length is < 1 or > 100) throw new AppException("Title must be between 1 and 100 characters.", 400);
        if (!Allowed.Contains(request.Priority)) throw new AppException("Priority must be one of: Low, Medium, High.", 400);
        var task = new TaskItem { Id = Guid.NewGuid(), UserId = _user.UserId, Title = title, Priority = Enum.Parse<TaskPriority>(request.Priority), IsCompleted = false, CreatedAt = DateTime.UtcNow };
        await _tasks.AddAsync(task, cancellationToken); await _uow.SaveChangesAsync(cancellationToken);
        return new TaskDto(task.Id, task.Title, task.Priority.ToString(), task.IsCompleted, task.CreatedAt);
    }
}

