namespace FocusFlow.Application.DTOs.Tasks;

public sealed record TaskDto(Guid Id, string Title, string Priority, bool IsCompleted, DateTime CreatedAt);
