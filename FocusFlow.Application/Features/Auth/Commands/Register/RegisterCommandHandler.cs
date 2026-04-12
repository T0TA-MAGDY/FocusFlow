using FocusFlow.Application.Common.Exceptions;
using FocusFlow.Application.Common.Interfaces;
using FocusFlow.Application.DTOs.Auth;
using FocusFlow.Domain.Entities;
using FocusFlow.Domain.Interfaces;
using MediatR;

namespace FocusFlow.Application.Features.Auth.Commands.Register;

public sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponseDto>
{
    private readonly IUserRepository _users; private readonly IPasswordHasherService _hasher; private readonly IJwtProvider _jwt; private readonly IUnitOfWork _uow;
    public RegisterCommandHandler(IUserRepository users, IPasswordHasherService hasher, IJwtProvider jwt, IUnitOfWork uow) { _users = users; _hasher = hasher; _jwt = jwt; _uow = uow; }
    public async Task<AuthResponseDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password)) throw new AppException("Username, email, and password are required.", 400);
        var email = request.Email.Trim().ToLowerInvariant(); var username = request.Username.Trim();
        if (await _users.GetByEmailAsync(email, cancellationToken) is not null) throw new AppException("Email is already registered.", 400);
        if (await _users.GetByUsernameAsync(username, cancellationToken) is not null) throw new AppException("Username is already taken.", 400);
        var user = new User { Id = Guid.NewGuid(), Username = username, Email = email, PasswordHash = _hasher.HashPassword(request.Password), CreatedAt = DateTime.UtcNow };
        await _users.AddAsync(user, cancellationToken); await _uow.SaveChangesAsync(cancellationToken);
        return new AuthResponseDto(user.Id, user.Username, user.Email, _jwt.GenerateToken(user.Id, user.Username, user.Email));
    }
}

