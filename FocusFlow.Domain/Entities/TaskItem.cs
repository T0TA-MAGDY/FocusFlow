using FocusFlow.Domain.Enums;

namespace FocusFlow.Domain.Entities;

public sealed class TaskItem
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public TaskPriority Priority { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public User User { get; set; } = null!;
    public ICollection<FocusSession> FocusSessions { get; set; } = new List<FocusSession>();
}
