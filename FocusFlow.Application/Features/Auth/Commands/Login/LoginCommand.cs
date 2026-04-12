using FocusFlow.Application.DTOs.Auth;
using MediatR;

namespace FocusFlow.Application.Features.Auth.Commands.Login;

public sealed record LoginCommand(string Email, string Password) : IRequest<AuthResponseDto>;
