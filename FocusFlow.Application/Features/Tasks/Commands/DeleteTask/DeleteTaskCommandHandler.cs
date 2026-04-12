using FocusFlow.Application.Common.Exceptions;
using FocusFlow.Application.Common.Interfaces;
using FocusFlow.Domain.Interfaces;
using MediatR;

namespace FocusFlow.Application.Features.Tasks.Commands.DeleteTask;

public sealed class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, Unit>
{
    private readonly ITaskRepository _tasks; private readonly ICurrentUserService _user; private readonly IUnitOfWork _uow;
    public DeleteTaskCommandHandler(ITaskRepository tasks, ICurrentUserService user, IUnitOfWork uow) { _tasks = tasks; _user = user; _uow = uow; }
    public async Task<Unit> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _tasks.GetByIdForUserAsync(request.TaskId, _user.UserId, cancellationToken) ?? throw new AppException("Task not found.", 404);
        _tasks.Remove(task); await _uow.SaveChangesAsync(cancellationToken); return Unit.Value;
    }
}

