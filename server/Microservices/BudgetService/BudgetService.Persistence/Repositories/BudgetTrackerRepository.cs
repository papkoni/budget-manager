using BudgetService.Domain.Entities;
using BudgetService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BudgetService.Persistence.Repositories;

public class BudgetTrackerRepository
{
    private readonly IBudgetServiceDbContext _context;

    public BudgetTrackerRepository(IBudgetServiceDbContext context)
    {
        _context = context;
    }
}