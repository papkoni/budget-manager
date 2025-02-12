using BudgetService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BudgetService.Domain.Interfaces;

public interface IBudgetServiceDbContext
{
    DbSet<BudgetCategoryEntity> BudgetCategories { get; }
    DbSet<BudgetEntity> Budgets { get; }
    DbSet<BudgetTrackerEntity> BudgetTrackers { get; }
    DbSet<CategoryEntity> Categories { get; }
    DbSet<GoalEntity> Goals { get; }
}