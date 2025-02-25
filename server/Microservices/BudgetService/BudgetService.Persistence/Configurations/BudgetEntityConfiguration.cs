using BudgetService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BudgetService.Persistence.Configurations;

public class BudgetEntityConfiguration : IEntityTypeConfiguration<BudgetEntity>
{
    public void Configure(EntityTypeBuilder<BudgetEntity> builder)
    {
        builder.ToTable("Budgets");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Amount)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(b => b.Currency)
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(b => b.PeriodType)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(b => b.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(b => b.StartDate)
            .IsRequired()
            .HasConversion(
            v => v.ToUniversalTime(), // Convert to UTC before save
            v => DateTime.SpecifyKind(v, DateTimeKind.Utc)); // Convert to UTC after upload
        
        builder.Property(b => b.CreatedAt)
            .IsRequired()
            .HasConversion(
                v => v.ToUniversalTime(), // Convert to UTC before save
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc)); // Convert to UTC after upload
        
        builder.Property(b => b.EndDate)
            .IsRequired()
            .HasConversion(
                v => v.ToUniversalTime(), // Convert to UTC before save
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc)); // Convert to UTC after upload
        
        builder.Property(b => b.UpdatedAt)
            .IsRequired()
            .HasConversion(
                v => v.ToUniversalTime(), // Convert to UTC before save
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc)); // Convert to UTC after upload

        builder.HasMany(b => b.BudgetCategories)
            .WithOne(bc => bc.Budget)
            .HasForeignKey(bc => bc.BudgetId);
    }
}