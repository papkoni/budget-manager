using BudgetService.Domain.Entities;
using BudgetService.Persistence.Configurations;
using BudgetService.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BudgetService.Persistence;

public class BudgetServiceDbContext : DbContext, IBudgetServiceDbContext
{
    public BudgetServiceDbContext(DbContextOptions<BudgetServiceDbContext> options) : base(options)
    {
    }
    
    public DbSet<BudgetCategoryEntity> BudgetCategories { get; set; }
    public DbSet<BudgetEntity> Budgets { get; set; }
    public DbSet<CategoryEntity> Categories { get; set; }
    public DbSet<GoalEntity> Goals { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BudgetCategoryEntityConfiguration());
        modelBuilder.ApplyConfiguration(new BudgetEntityConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryEntityConfiguration());
        modelBuilder.ApplyConfiguration(new GoalEntityConfiguration());
    }
} 
