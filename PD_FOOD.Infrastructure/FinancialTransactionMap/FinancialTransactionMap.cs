using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PD_FOOD.Domain.Entities;

namespace PD_FOOD.Infrastructure.FinancialTransactionMap
{
    public class FinancialTransactionMap : IEntityTypeConfiguration<FinancialTransaction>
    {
        public void Configure(EntityTypeBuilder<FinancialTransaction> builder)
        {
            builder.ToTable("FinancialTransactions");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();
            builder.Property(c => c.Type);
            builder.Property(c => c.Date).IsRequired();
            builder.Property(c => c.Description)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(c => c.Amount)
                .HasColumnType("DECIMAL(18,2)")
                .IsRequired();
        }
    }
}