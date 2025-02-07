using BudgetService.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BudgetService.Persistence.Configurations;

public class BudgetCategoryEntityConfiguration : IEntityTypeConfiguration<BudgetCategoryEntity>
{
    public void Configure(EntityTypeBuilder<BudgetCategoryEntity> builder)
    {
        builder.ToTable("BudgetCategories");

        builder.HasKey(bc => bc.Id);

        builder.Property(bc => bc.Amount)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(bc => bc.Spent)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.HasOne(bc => bc.Budget)
            .WithMany(b => b.BudgetCategories)
            .HasForeignKey(bc => bc.BudgetId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(bc => bc.Category)
            .WithMany(c => c.BudgetCategories)
            .HasForeignKey(bc => bc.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}