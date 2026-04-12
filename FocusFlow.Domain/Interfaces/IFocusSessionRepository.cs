using FocusFlow.Domain.Entities;

namespace FocusFlow.Domain.Interfaces;

public interface IFocusSessionRepository
{
    Task AddAsync(FocusSession session, CancellationToken cancellationToken);
}
