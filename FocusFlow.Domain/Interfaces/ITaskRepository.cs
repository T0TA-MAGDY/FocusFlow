using FocusFlow.Domain.Entities;

namespace FocusFlow.Domain.Interfaces;

public interface ITaskRepository
{
    Task<IReadOnlyList<TaskItem>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<TaskItem?> GetByIdForUserAsync(Guid taskId, Guid userId, CancellationToken cancellationToken);
    Task AddAsync(TaskItem task, CancellationToken cancellationToken);
    void Remove(TaskItem task);
}
