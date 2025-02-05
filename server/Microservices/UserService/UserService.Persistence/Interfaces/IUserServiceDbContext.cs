using Microsoft.EntityFrameworkCore;
using UserService.Persistence.Models;

namespace UserService.Persistence.Interfaces;

public interface IUserServiceDbContext
{
    DbSet<UserModel> Users { get; }
    DbSet<RefreshTokenModel> RefreshTokens { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}