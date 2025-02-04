using System.Reflection;
using Microsoft.EntityFrameworkCore;
using UserService.Application.Interfaces;
using UserService.Application.Models;

namespace UserService.Persistence;

public class UserServiceDbContext: DbContext, IUserServiceDbContext
{
    public DbSet<UserModel> Users { get; set; }
    public DbSet<RefreshTokenModel> RefreshTokens { get; set; }

    public UserServiceDbContext(DbContextOptions<UserServiceDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}