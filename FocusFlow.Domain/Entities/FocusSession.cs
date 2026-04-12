using FocusFlow.Domain.Enums;

namespace FocusFlow.Domain.Entities;

public sealed class FocusSession
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid TaskId { get; set; }
    public DateTime StartTime { get; set; }
    public int DurationSeconds { get; set; }
    public User User { get; set; } = null!;
    public TaskItem Task { get; set; } = null!;
}
