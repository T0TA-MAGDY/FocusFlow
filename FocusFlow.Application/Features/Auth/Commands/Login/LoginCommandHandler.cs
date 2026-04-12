using FocusFlow.Application.Common.Exceptions;
using FocusFlow.Application.Common.Interfaces;
using FocusFlow.Application.DTOs.Auth;
using FocusFlow.Domain.Interfaces;
using MediatR;

namespace FocusFlow.Application.Features.Auth.Commands.Login;

public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponseDto>
{
    private readonly IUserRepository _users; private readonly IPasswordHasherService _hasher; private readonly IJwtProvider _jwt;
    public LoginCommandHandler(IUserRepository users, IPasswordHasherService hasher, IJwtProvider jwt) { _users = users; _hasher = hasher; _jwt = jwt; }
    public async Task<AuthResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password)) throw new AppException("Email and password are required.", 400);
        var user = await _users.GetByEmailAsync(request.Email.Trim().ToLowerInvariant(), cancellationToken);
        if (user is null || !_hasher.VerifyPassword(user.PasswordHash, request.Password)) throw new AppException("Invalid credentials.", 401);
        return new AuthResponseDto(user.Id, user.Username, user.Email, _jwt.GenerateToken(user.Id, user.Username, user.Email));
    }
}

