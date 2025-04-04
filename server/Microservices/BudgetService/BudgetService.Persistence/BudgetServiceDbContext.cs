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
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BudgetCategoryEntityConfiguration());
        modelBuilder.ApplyConfiguration(new BudgetEntityConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryEntityConfiguration());
        modelBuilder.ApplyConfiguration(new GoalEntityConfiguration());
    }
} 
