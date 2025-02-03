using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Application.Models;

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
            v => v.ToUniversalTime(), // Преобразование к UTC перед сохранением
            v => DateTime.SpecifyKind(v, DateTimeKind.Utc)); // Преобразование к UTC при загрузке;

        builder.Property(r => r.ExpiryDate)
            .IsRequired()
            .HasConversion(
            v => v.ToUniversalTime(), // Преобразование к UTC перед сохранением
            v => DateTime.SpecifyKind(v, DateTimeKind.Utc)); // Преобразование к UTC при загрузке;

        

        builder.HasOne(r => r.User)
            .WithOne(u => u.RefreshToken)
            .HasForeignKey<RefreshTokenModel>(r => r.UserId);
    }
}