using BudgetService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BudgetService.Persistence.Configurations;

public class CategoryEntityConfiguration : IEntityTypeConfiguration<CategoryEntity>
{
    public void Configure(EntityTypeBuilder<CategoryEntity> builder)
    {
        builder.ToTable("Categories");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.GlobalLimit)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(c => c.GlobalSpent)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(c => c.CreatedAt)
            .IsRequired()
            .HasConversion(
                v => v.ToUniversalTime(), // Convert to UTC before save
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc)); // Convert to UTC after upload

        builder.HasMany(c => c.BudgetCategories)
            .WithOne(bc => bc.Category)
            .HasForeignKey(bc => bc.CategoryId);
    }
}