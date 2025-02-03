using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Application.Enums;
using UserService.Application.Models;

namespace UserService.Persistence.Configurations;

public partial class UserConfiguration : IEntityTypeConfiguration<UserModel>
{
    public void Configure(EntityTypeBuilder<UserModel> builder)
    {
        builder.ToTable("User");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(u => u.PasswordHash)
            .IsRequired();

        builder.Property(u => u.Role)
            .HasConversion(
                role => role.ToString(),
                value => Enum.Parse<Role>(value)
            );

        builder.Property(d => d.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasOne(u => u.RefreshToken)
            .WithOne(r => r.User)
            .HasForeignKey<RefreshTokenModel>(r => r.UserId);


    }
}