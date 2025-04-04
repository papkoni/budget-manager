using BudgetService.Domain.Entities;
using MediatR;

namespace BudgetService.Application.Handlers.Queries.Category.GetCategoryById;

public class GetCategoryByIdQuery(Guid id) : IRequest<CategoryEntity>
{
    public Guid Id { get; private set; } = id;
}