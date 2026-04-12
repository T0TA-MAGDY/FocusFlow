using FocusFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FocusFlow.Infrastructure.Persistence.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> b)
    {
        b.ToTable("Users"); b.HasKey(x => x.Id);
        b.Property(x => x.Username).HasMaxLength(50).IsRequired();
        b.Property(x => x.Email).HasMaxLength(255).IsRequired();
        b.Property(x => x.PasswordHash).IsRequired();
        b.Property(x => x.CreatedAt).IsRequired();
        b.HasIndex(x => x.Email).IsUnique(); b.HasIndex(x => x.Username).IsUnique();
    }
}
