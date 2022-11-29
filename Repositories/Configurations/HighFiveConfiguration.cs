using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public sealed class HighFiveConfiguration : IEntityTypeConfiguration<HighFive>
    {
        public void Configure(EntityTypeBuilder<HighFive> builder)
        {
            // PK
            builder.HasKey(p => p.Id);

            // Columns
            builder.Property(c => c.UserIdTo).IsRequired();

            // Relationships
            builder.HasOne(p => p.User)
                .WithMany(p => p.HighFives)
                .HasForeignKey(p => p.UserIdTo);
        }
    }
}
