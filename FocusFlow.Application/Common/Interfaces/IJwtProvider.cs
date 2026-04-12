namespace FocusFlow.Application.Common.Interfaces;

public interface IJwtProvider
{
    string GenerateToken(Guid userId, string username, string email);
}
