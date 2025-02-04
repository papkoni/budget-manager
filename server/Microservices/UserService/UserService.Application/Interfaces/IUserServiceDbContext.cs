using UserService.Application.Models;
using Microsoft.EntityFrameworkCore; 

namespace UserService.Application.Interfaces;

public interface IUserServiceDbContext
{
    DbSet<UserModel> Users { get; }
    DbSet<RefreshTokenModel> RefreshTokens { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}