using FocusFlow.Application.Common.Exceptions;
using FocusFlow.Application.Common.Interfaces;
using FocusFlow.Application.DTOs.Sessions;
using FocusFlow.Domain.Entities;
using FocusFlow.Domain.Enums;
using FocusFlow.Domain.Interfaces;
using MediatR;

namespace FocusFlow.Application.Features.Sessions.Commands.CreateSession;

public sealed class CreateSessionCommandHandler : IRequestHandler<CreateSessionCommand, FocusSessionDto>
{
    private readonly ITaskRepository _tasks; private readonly IFocusSessionRepository _sessions; private readonly ICurrentUserService _user; private readonly IUnitOfWork _uow;
    public CreateSessionCommandHandler(ITaskRepository tasks, IFocusSessionRepository sessions, ICurrentUserService user, IUnitOfWork uow) { _tasks = tasks; _sessions = sessions; _user = user; _uow = uow; }
    public async Task<FocusSessionDto> Handle(CreateSessionCommand request, CancellationToken cancellationToken)
    {
        if (request.TaskId == Guid.Empty)
        {
            throw new AppException("TaskId is required.", 400);
        }

        if (request.DurationSeconds <= 0 || request.DurationSeconds > 86400) throw new AppException("DurationSeconds must be between 1 and 86400.", 400);
        _ = await _tasks.GetByIdForUserAsync(request.TaskId, _user.UserId, cancellationToken) ?? throw new AppException("Task does not exist or does not belong to the current user.", 400);
        var session = new FocusSession { Id = Guid.NewGuid(), UserId = _user.UserId, TaskId = request.TaskId, StartTime = request.StartTime, DurationSeconds = request.DurationSeconds};
        await _sessions.AddAsync(session, cancellationToken); await _uow.SaveChangesAsync(cancellationToken);
        return new FocusSessionDto(session.Id, session.TaskId, session.StartTime, session.DurationSeconds);
    }
}

