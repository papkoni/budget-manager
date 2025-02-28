using BudgetService.Domain.Entities;
using MediatR;

namespace BudgetService.Application.Handlers.Commands.Category.CreateCategory;

public record CreateCategoryCommand(
    string Name,
    decimal GlobalLimit,
    decimal GlobalSpent
    ) : IRequest<CategoryEntity>;