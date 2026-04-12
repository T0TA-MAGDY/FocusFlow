using FocusFlow.Domain.Entities;
using FocusFlow.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FocusFlow.Infrastructure.Persistence.Repositories;

public sealed class TaskRepository : ITaskRepository
{
    private readonly FocusFlowDbContext _db;
    public TaskRepository(FocusFlowDbContext db) => _db = db;
    public async Task<IReadOnlyList<TaskItem>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken) => await _db.Tasks.Where(t => t.UserId == userId).OrderByDescending(t => t.CreatedAt).ToListAsync(cancellationToken);
    public Task<TaskItem?> GetByIdForUserAsync(Guid taskId, Guid userId, CancellationToken cancellationToken) => _db.Tasks.FirstOrDefaultAsync(t => t.Id == taskId && t.UserId == userId, cancellationToken);
    public Task AddAsync(TaskItem task, CancellationToken cancellationToken) => _db.Tasks.AddAsync(task, cancellationToken).AsTask();
    public void Remove(TaskItem task) => _db.Tasks.Remove(task);
}
