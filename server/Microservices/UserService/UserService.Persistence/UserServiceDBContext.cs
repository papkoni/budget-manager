using System.Reflection;
using Microsoft.EntityFrameworkCore;
using UserService.Persistence.Interfaces;
using UserService.Persistence.Models;

namespace UserService.Persistence;

public class UserServiceDbContext: DbContext, IUserServiceDbContext
{
    public UserServiceDbContext(DbContextOptions<UserServiceDbContext> options) : base(options)
    {
    }
    
    public DbSet<UserModel> Users { get; set; }
    public DbSet<RefreshTokenModel> RefreshTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}