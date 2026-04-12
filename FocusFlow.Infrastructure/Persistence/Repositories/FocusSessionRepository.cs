using FocusFlow.Domain.Entities;
using FocusFlow.Domain.Interfaces;

namespace FocusFlow.Infrastructure.Persistence.Repositories;

public sealed class FocusSessionRepository : IFocusSessionRepository
{
    private readonly FocusFlowDbContext _db;
    public FocusSessionRepository(FocusFlowDbContext db) => _db = db;
    public Task AddAsync(FocusSession session, CancellationToken cancellationToken) => _db.FocusSessions.AddAsync(session, cancellationToken).AsTask();
}
