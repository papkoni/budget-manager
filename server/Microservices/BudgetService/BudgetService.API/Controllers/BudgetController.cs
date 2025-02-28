using BudgetService.Application.Handlers.Commands.Budget.CreateBudget;
using BudgetService.Application.Handlers.Commands.Budget.DeleteBudget;
using BudgetService.Application.Handlers.Commands.Budget.UpdateBudget;
using BudgetService.Application.Handlers.Commands.BudgetCategory.CreateBudgetCategory;
using BudgetService.Application.Handlers.Commands.BudgetCategory.DeleteBudgetCategory;
using BudgetService.Application.Handlers.Commands.BudgetCategory.UpdateBudgetCategory;
using BudgetService.Application.Handlers.Queries.Budget.GetActiveBudgetsByUserId;
using BudgetService.Application.Handlers.Queries.Budget.GetBudgetById;
using BudgetService.Application.Handlers.Queries.Budget.GetBudgetsByUserId;
using BudgetService.Application.Handlers.Queries.BudgetCategory.GetBudgetCategoryByBudgetId;
using BudgetService.Application.Handlers.Queries.BudgetCategory.GetBudgetCategoryById;
using BudgetService.Application.DTO;
using BudgetService.Application.Handlers.Queries.BudgetCategory.GetBudgetCategoryByIdBudgetAndCategory;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BudgetService.API.Controllers;

[ApiController]
[Route("budgets")]
public class BudgetController(IMediator mediator): ControllerBase
{
    [HttpGet("/users/{userId}/budgets")]
    public async Task<IActionResult> GetByUserId([FromRoute] Guid userId, CancellationToken cancellationToken)
    {
        var budgets = await mediator.Send(new GetBudgetsByUserIdQuery(userId), cancellationToken);

        return Ok(budgets);
    }
    //сделать эндпоинт для получения локальной категории по 2 айди
    
    [HttpGet("/budgets/{budgetId:Guid}/categories/{categoryId:Guid}")]
    public async Task<IActionResult> GetBudgetCategoryById(
        [FromRoute] Guid budgetId, 
        [FromRoute] Guid categoryId, 
        CancellationToken cancellationToken)
    {
        var budgetCategory = await mediator.Send(new GetBudgetCategoryByIdBudgetAndCategoryQuery(budgetId, categoryId), cancellationToken);
    
        return Ok(budgetCategory);
    }
    
    [HttpGet("categories/{categoryId}")]
    public async Task<IActionResult> GetBudgetCategoryById([FromRoute] Guid categoryId, CancellationToken cancellationToken)
    {
        var budgetCategory = await mediator.Send(new GetBudgetCategoryByIdQuery(categoryId), cancellationToken);
    
        return Ok(budgetCategory);
    }
    
    [HttpGet("{budgetId:Guid}/categories")]
    public async Task<IActionResult> GetBudgetCategoryByBudgetId([FromRoute] Guid budgetId, CancellationToken cancellationToken)
    {
        var budgetCategory = await mediator.Send(new GetBudgetCategoryByBudgetIdQuery(budgetId), cancellationToken);
    
        return Ok(budgetCategory);
    }
    
    [HttpGet("{budgetId:Guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var budget = await mediator.Send(new GetBudgetByIdQuery(id), cancellationToken);

        return Ok(budget);
    }

    [HttpGet("/users/{userId:Guid}/budgets/active")]
    public async Task<IActionResult> GetActiveUserBudgets([FromRoute] Guid userId,[FromQuery] DateTime currentDate, CancellationToken cancellationToken)
    {
        var budgets = await mediator.Send(new GetActiveBudgetsByUserIdQuery(userId, currentDate), cancellationToken);
    
        return Ok(budgets);
    }
    
    [HttpPost("categories")]
    public async Task<IActionResult> Create([FromBody] CreateBudgetCategoryCommand request, CancellationToken cancellationToken)
    {
        var budgetCategory = await mediator.Send(request, cancellationToken);
    
        return Ok(budgetCategory);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBudgetCommand request, CancellationToken cancellationToken)
    {
        var budget = await mediator.Send(request, cancellationToken);

        return Ok(budget);
    }

    [HttpPut("{budgetId:Guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateBudgetDto dto, CancellationToken cancellationToken)
    {
        await mediator.Send(new UpdateBudgetCommand(id, dto), cancellationToken);
    
        return NoContent();
    }
    
    [HttpPut("categories/{categoryId:Guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid categoryId, [FromBody] UpdateBudgetCategoryDto dto, CancellationToken cancellationToken)
    {
        await mediator.Send(new UpdateBudgetCategoryCommand(categoryId, dto), cancellationToken);
    
        return NoContent();
    }
    
    [HttpDelete("{budgetId:Guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteBudgetCommand(id), cancellationToken);
    
        return NoContent();
    }
    
    [HttpDelete("categories/{categoryId}")]
    public async Task<IActionResult> DeleteBudgetCategory([FromRoute] Guid categoryId, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteBudgetCategoryCommand(categoryId), cancellationToken);

        return NoContent();
    }
}