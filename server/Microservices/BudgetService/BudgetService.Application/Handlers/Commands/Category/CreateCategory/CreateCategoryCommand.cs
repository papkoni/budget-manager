using BudgetService.Domain.Entities;
using MediatR;

namespace BudgetService.Application.Handlers.Commands.Category.CreateCategory;

public class CreateCategoryCommand(
    string name,
    decimal globalLimit,
    decimal globalSpent
    ) : IRequest<CategoryEntity>
{
    public string Name { get; private set; } = name;                     // Название категории
    public decimal GlobalLimit { get; private set; } = globalLimit;      // Глобальный лимит
    public decimal GlobalSpent { get; private set; } = globalSpent;      // Глобально потраченная сумма
}
