using FocusFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FocusFlow.Infrastructure.Persistence.Configurations;

public sealed class FocusSessionConfiguration : IEntityTypeConfiguration<FocusSession>
{
    public void Configure(EntityTypeBuilder<FocusSession> b)
    {
        b.ToTable("FocusSessions"); b.HasKey(x => x.Id);
        b.Property(x => x.StartTime).IsRequired(); b.Property(x => x.DurationSeconds).IsRequired();
        b.HasOne(x => x.User).WithMany(u => u.FocusSessions).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
        b.HasOne(x => x.Task).WithMany(t => t.FocusSessions).HasForeignKey(x => x.TaskId).OnDelete(DeleteBehavior.Cascade);
        b.HasIndex(x => x.UserId); b.HasIndex(x => x.TaskId);
    }
}
