using BudgetService.Domain.Entities;
using MediatR;

namespace BudgetService.Application.Handlers.Queries.Category.GetCategories;

public class GetCategoriesQuery: IRequest<List<CategoryEntity>>
{
}