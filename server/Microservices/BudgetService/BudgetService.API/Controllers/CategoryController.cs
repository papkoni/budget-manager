using BudgetService.Application.Handlers.Commands.Category.CreateCategory;
using BudgetService.Application.Handlers.Commands.Category.DeleteCategory;
using BudgetService.Application.Handlers.Commands.Category.UpdateCategory;
using BudgetService.Application.Handlers.Queries.Category.GetCategories;
using BudgetService.Application.Handlers.Queries.Category.GetCategoryById;
using BudgetService.Application.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BudgetService.API.Controllers;

[ApiController]
[Route("categories")]
public class CategoryController(IMediator mediator): ControllerBase
{
    [HttpGet("{categoryId:Guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid categoryId, CancellationToken cancellationToken)
    {
        var category = await mediator.Send(new GetCategoryByIdQuery(categoryId), cancellationToken);
    
        return Ok(category);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllCategories(CancellationToken cancellationToken)
    {
        var categories = await mediator.Send(new GetCategoriesQuery(), cancellationToken);
        
        return Ok(categories);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await mediator.Send(request, cancellationToken);

        return Ok(category);
    }
    
    [HttpPut("{categoryId:Guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid categoryId, [FromBody] UpdateCategoryDto dto, CancellationToken cancellationToken)
    {
        await mediator.Send(new UpdateCategoryCommand(categoryId, dto), cancellationToken);
    
        return NoContent();
    }
    
    [HttpDelete("{categoryId:Guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid categoryId, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteCategoryCommand(categoryId), cancellationToken);
    
        return NoContent();
    }
}