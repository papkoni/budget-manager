using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Persistence.Models;

namespace UserService.Persistence.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshTokenModel>
{
    public void Configure(EntityTypeBuilder<RefreshTokenModel> builder)
    {
        builder.ToTable("RefreshToken");
        
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Token)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(r => r.CreatedDate)
            .IsRequired()
            .HasConversion(
            v => v.ToUniversalTime(), // Convert to UTC before save
            v => DateTime.SpecifyKind(v, DateTimeKind.Utc)); // Convert to UTC after upload

        builder.Property(r => r.ExpiryDate)
            .IsRequired()
            .HasConversion(
            v => v.ToUniversalTime(),  // Convert to UTC before save
            v => DateTime.SpecifyKind(v, DateTimeKind.Utc)); // // Convert to UTC after upload
        
        builder.HasOne(r => r.User)
            .WithOne(u => u.RefreshToken)
            .HasForeignKey<RefreshTokenModel>(r => r.UserId);
    }
}