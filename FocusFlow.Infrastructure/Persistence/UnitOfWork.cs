using FocusFlow.Domain.Interfaces;

namespace FocusFlow.Infrastructure.Persistence;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly FocusFlowDbContext _db;
    public UnitOfWork(FocusFlowDbContext db) => _db = db;
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken) => _db.SaveChangesAsync(cancellationToken);
}
