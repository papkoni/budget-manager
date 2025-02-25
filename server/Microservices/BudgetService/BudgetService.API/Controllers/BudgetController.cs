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
using BudgetService.Domain.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BudgetService.API.Controllers;

[ApiController]
[Route("budgets")]
public class BudgetController(IMediator mediator): ControllerBase
{
    [HttpGet("/users/{userId}/budgets")]
    public async Task<IActionResult> GetByUserId([FromRoute] Guid userId)
    {
        var budgets = await mediator.Send(new GetBudgetsByUserIdQuery(userId));

        return Ok(budgets);
    }
    
    [HttpGet("categories/{id}")]
    public async Task<IActionResult> GetBudgetCategoryById([FromRoute] Guid id)
    {
        var budgetCategory = await mediator.Send(new GetBudgetCategoryByIdQuery(id));
    
        return Ok(budgetCategory);
    }
    
    [HttpGet("{budgetId:Guid}/categories")]
    public async Task<IActionResult> GetBudgetCategoryByBudgetId([FromRoute] Guid budgetId)
    {
        var budgetCategory = await mediator.Send(new GetBudgetCategoryByBudgetIdQuery(budgetId));
    
        return Ok(budgetCategory);
    }
    
    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var budget = await mediator.Send(new GetBudgetByIdQuery(id));

        return Ok(budget);
    }
    
    [HttpGet("/users/{userId:Guid}/budgets/date/{currentDate:DateTime}")]
    public async Task<IActionResult> GetActiveUserBudgets([FromRoute] Guid userId, DateTime currentDate)
    {
        var budgets = await mediator.Send(new GetActiveBudgetsByUserIdQuery(userId, currentDate));
    
        return Ok(budgets);
    }
    
    [HttpPost("categories")]
    public async Task<IActionResult> Create([FromBody] CreateBudgetCategoryCommand request)
    {
        var budgetCategory = await mediator.Send(request);
    
        return Ok(budgetCategory);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBudgetCommand request)
    {
        var budget = await mediator.Send(request);

        return Ok(budget);
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateBudgetDto dto)
    {
        await mediator.Send(new UpdateBudgetCommand(id, dto));
    
        return NoContent();
    }
    
    [HttpPut("categories/{id:Guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateBudgetCategoryDto dto)
    {
        await mediator.Send(new UpdateBudgetCategoryCommand(id, dto));
    
        return NoContent();
    }
    
    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await mediator.Send(new DeleteBudgetCommand(id));
    
        return NoContent();
    }
    
    [HttpDelete("categories/{id}")]
    public async Task<IActionResult> DeleteBudgetCategory([FromRoute] Guid id)
    {
        await mediator.Send(new DeleteBudgetCategoryCommand(id));

        return NoContent();
    }
}