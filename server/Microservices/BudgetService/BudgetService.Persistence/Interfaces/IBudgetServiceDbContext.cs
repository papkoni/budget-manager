using BudgetService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BudgetService.Persistence.Interfaces;

public interface IBudgetServiceDbContext
{
    DbSet<T> Set<T>() where T : class;
}