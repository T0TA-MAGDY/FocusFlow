namespace FocusFlow.Application.DTOs.Auth;

public sealed record AuthResponseDto(Guid UserId, string Username, string Email, string Token);
