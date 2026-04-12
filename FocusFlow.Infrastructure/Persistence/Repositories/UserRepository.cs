using FocusFlow.Domain.Entities;
using FocusFlow.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FocusFlow.Infrastructure.Persistence.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly FocusFlowDbContext _db;
    public UserRepository(FocusFlowDbContext db) => _db = db;
    public Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken) => _db.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    public Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken) => _db.Users.FirstOrDefaultAsync(u => u.Username == username, cancellationToken);
    public Task AddAsync(User user, CancellationToken cancellationToken) => _db.Users.AddAsync(user, cancellationToken).AsTask();
}
