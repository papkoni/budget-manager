using BudgetService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BudgetService.Persistence.Interfaces;

public interface IBudgetServiceDbContext
{
    DbSet<BudgetCategoryEntity> BudgetCategories { get; }
    DbSet<BudgetEntity> Budgets { get; }
    DbSet<CategoryEntity> Categories { get; }
    DbSet<GoalEntity> Goals { get; }
}