namespace FocusFlow.API.Contracts.Sessions;
public sealed record CreateSessionRequest(Guid TaskId, DateTime StartTime, int DurationSeconds);
