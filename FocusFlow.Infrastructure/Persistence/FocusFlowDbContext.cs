using FocusFlow.Domain.Entities;
using FocusFlow.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace FocusFlow.Infrastructure.Persistence;

public sealed class FocusFlowDbContext : DbContext
{
    public FocusFlowDbContext(DbContextOptions<FocusFlowDbContext> options) : base(options) {}
    public DbSet<User> Users => Set<User>();
    public DbSet<TaskItem> Tasks => Set<TaskItem>();
    public DbSet<FocusSession> FocusSessions => Set<FocusSession>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new TaskConfiguration());
        modelBuilder.ApplyConfiguration(new FocusSessionConfiguration());
    }
}
