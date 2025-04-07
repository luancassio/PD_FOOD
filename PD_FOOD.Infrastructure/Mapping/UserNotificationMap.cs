using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PD_FOOD.Domain.Entities;

namespace PD_FOOD.Infrastructure.Mapping
{
    internal class UserNotificationMap : IEntityTypeConfiguration<UserNotification>
    {
        public void Configure(EntityTypeBuilder<UserNotification> builder)
        {
            builder.ToTable("UserNotifications");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();
            builder.Property(c => c.Name)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(c => c.Email)
                .HasMaxLength(50)
                .IsRequired();
            builder.HasIndex(c => c.Email)
                   .IsUnique();
            builder.Property(c => c.IsActive)
                .IsRequired();
            builder.Property(c => c.Hour)
                .HasColumnType("TIME")
                .IsRequired();
        }
    }

}
