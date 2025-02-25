using BudgetService.Application.Handlers.Commands.Category.CreateCategory;
using BudgetService.Application.Handlers.Commands.Category.DeleteCategory;
using BudgetService.Application.Handlers.Commands.Category.UpdateCategory;
using BudgetService.Application.Handlers.Queries.Category.GetCategories;
using BudgetService.Application.Handlers.Queries.Category.GetCategoryById;
using BudgetService.Domain.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BudgetService.API.Controllers;

[ApiController]
[Route("categories")]
public class CategoryController(IMediator mediator): ControllerBase
{
    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var category = await mediator.Send(new GetCategoryByIdQuery(id));
    
        return Ok(category);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
        var categories = await mediator.Send(new GetCategoriesQuery());
        return Ok(categories);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryCommand request)
    {
        var category = await mediator.Send(request);

        return Ok(category);
    }
    
    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateCategoryDto dto)
    {
        await mediator.Send(new UpdateCategoryCommand(id, dto));
    
        return NoContent();
    }
    
    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await mediator.Send(new DeleteCategoryCommand(id));
    
        return NoContent();
    }
}