namespace FocusFlow.Application.DTOs.Sessions;

public sealed record FocusSessionDto(Guid Id, Guid TaskId, DateTime StartTime, int DurationSeconds);
