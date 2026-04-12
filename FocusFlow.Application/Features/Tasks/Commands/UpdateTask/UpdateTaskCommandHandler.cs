using FocusFlow.Application.Common.Exceptions;
using FocusFlow.Application.Common.Interfaces;
using FocusFlow.Application.DTOs.Tasks;
using FocusFlow.Domain.Interfaces;
using MediatR;

namespace FocusFlow.Application.Features.Tasks.Commands.UpdateTask;

public sealed class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, TaskDto>
{
    private readonly ITaskRepository _tasks; private readonly ICurrentUserService _user; private readonly IUnitOfWork _uow;
    public UpdateTaskCommandHandler(ITaskRepository tasks, ICurrentUserService user, IUnitOfWork uow) { _tasks = tasks; _user = user; _uow = uow; }
    public async Task<TaskDto> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _tasks.GetByIdForUserAsync(request.TaskId, _user.UserId, cancellationToken) ?? throw new AppException("Task not found.", 404);
        task.IsCompleted = request.IsCompleted; await _uow.SaveChangesAsync(cancellationToken);
        return new TaskDto(task.Id, task.Title, task.Priority.ToString(), task.IsCompleted, task.CreatedAt);
    }
}

