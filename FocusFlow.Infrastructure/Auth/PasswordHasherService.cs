using FocusFlow.Application.Common.Interfaces;
using FocusFlow.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace FocusFlow.Infrastructure.Auth;

public sealed class PasswordHasherService : IPasswordHasherService
{
    private readonly PasswordHasher<User> _hasher = new();
    public string HashPassword(string password) => _hasher.HashPassword(new User(), password);
    public bool VerifyPassword(string hashedPassword, string providedPassword)
        => _hasher.VerifyHashedPassword(new User(), hashedPassword, providedPassword) is PasswordVerificationResult.Success or PasswordVerificationResult.SuccessRehashNeeded;
}
