using FocusFlow.Application.DTOs.Auth;
using MediatR;

namespace FocusFlow.Application.Features.Auth.Commands.Register;

public sealed record RegisterCommand(string Username, string Email, string Password) : IRequest<AuthResponseDto>;
