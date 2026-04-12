using FocusFlow.Application.DTOs.Sessions;
using MediatR;

namespace FocusFlow.Application.Features.Sessions.Commands.CreateSession;

public sealed record CreateSessionCommand(Guid TaskId, DateTime StartTime, int DurationSeconds) : IRequest<FocusSessionDto>;
