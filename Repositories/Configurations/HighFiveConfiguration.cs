using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace Infrastructure.Configurations
{
    [ExcludeFromCodeCoverage]
    public sealed class HighFiveConfiguration : IEntityTypeConfiguration<HighFive>
    {
        public void Configure(EntityTypeBuilder<HighFive> builder)
        {
            // PK
            builder.HasKey(p => p.Id);

            // Columns
            builder.Property(c => c.UserIdTo).IsRequired();
            builder.Property(c => c.UserNameTo).IsRequired();

            // Relationships
            builder.HasOne(p => p.User)
                .WithMany(p => p.HighFives)
                .HasForeignKey(p => p.UserIdTo);
        }
    }
}
