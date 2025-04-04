using BudgetService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BudgetService.Persistence.Configurations;

public class GoalEntityConfiguration : IEntityTypeConfiguration<GoalEntity>
{
    public void Configure(EntityTypeBuilder<GoalEntity> builder)
    {
        builder.HasKey(bg => bg.Id);

        builder.Property(bg => bg.TargetAmount)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(bg => bg.CurrentAmount)
            .HasColumnType("decimal(18,2)")
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(bg => bg.Deadline)
            .IsRequired()
            .HasConversion(
            v => v.ToUniversalTime(), // Convert to UTC before save
            v => DateTime.SpecifyKind(v, DateTimeKind.Utc)); // Convert to UTC after upload
        
        builder.Property(bg => bg.CreatedAt)
            .IsRequired()
            .HasConversion(
                v => v.ToUniversalTime(), // Convert to UTC before save
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc)); // Convert to UTC after upload
    }
}