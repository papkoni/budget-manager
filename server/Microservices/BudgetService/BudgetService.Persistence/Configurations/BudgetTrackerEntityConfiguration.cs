using BudgetService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BudgetService.Persistence.Configurations;

public class BudgetTrackerEntityConfiguration : IEntityTypeConfiguration<BudgetTrackerEntity>
{
    public void Configure(EntityTypeBuilder<BudgetTrackerEntity> builder)
    {
        builder.ToTable("BudgetTrackers");

        builder.HasKey(bt => bt.Id);

        builder.Property(bt => bt.CurrentSpent)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(bt => bt.CurrentBalance)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(bt => bt.LastUpdated)
            .IsRequired();

        builder.HasOne<BudgetEntity>()
            .WithMany()
            .HasForeignKey(bt => bt.BudgetId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}