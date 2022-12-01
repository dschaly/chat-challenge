using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace Infrastructure.Configurations
{
    [ExcludeFromCodeCoverage]
    public sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // PK
            builder.HasKey(p => p.Id);

            // Columns
            builder.Property(c => c.UserName).IsRequired();
        }
    }
}
