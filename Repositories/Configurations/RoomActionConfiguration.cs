using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace Infrastructure.Configurations
{
    [ExcludeFromCodeCoverage]
    public sealed class RoomActionConfiguration : IEntityTypeConfiguration<RoomAction>
    {
        public void Configure(EntityTypeBuilder<RoomAction> builder)
        {
            // PK
            builder.HasKey(p => p.Id);

            // Columns
            builder.Property(c => c.UserId).IsRequired();
            builder.Property(c => c.ActionId).IsRequired();
            builder.Property(c => c.CommentId);
            builder.Property(c => c.HighFiveId);
            

            // Relationships
            builder.HasOne(p => p.User)
                .WithMany(p => p.RoomActions)
                .HasForeignKey(p => p.UserId);

            builder.HasOne(p => p.Comment)
                .WithOne(p => p.RoomAction);

            builder.HasOne(p => p.HighFive)
                .WithOne(p => p.RoomAction);
        }
    }
}
