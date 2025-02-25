using BudgetService.Application.Handlers.Commands.Goal.CreateGoal;
using BudgetService.Application.Handlers.Commands.Goal.DeleteGoal;
using BudgetService.Application.Handlers.Commands.Goal.UpdateGoal;
using BudgetService.Application.Handlers.Queries.Goal.GetGoalById;
using BudgetService.Application.Handlers.Queries.Goal.GetGoalByUserId;
using BudgetService.Domain.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BudgetService.API.Controllers;

[ApiController]
[Route("goals")]
public class GoalController(IMediator mediator): ControllerBase
{
    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var goal = await mediator.Send(new GetGoalByIdQuery(id));

        return Ok(goal);
    }
    
    [HttpGet("/users/{userId:Guid}/goals")]
    public async Task<IActionResult> GetByUserId([FromRoute] Guid userId)
    {
        var goals = await mediator.Send(new GetGoalsByUserIdQuery(userId));

        return Ok(goals);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateGoalCommand request)
    {
        var goal = await mediator.Send(request);

        return Ok(goal);
    }
    
    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateGoalDto dto)
    {
        await mediator.Send(new UpdateGoalCommand(id, dto));
    
        return NoContent();
    }
    
    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await mediator.Send(new DeleteGoalCommand(id));

        return NoContent();
    }
}