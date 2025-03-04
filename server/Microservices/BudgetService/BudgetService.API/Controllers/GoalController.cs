using BudgetService.Application.Handlers.Commands.Goal.CreateGoal;
using BudgetService.Application.Handlers.Commands.Goal.DeleteGoal;
using BudgetService.Application.Handlers.Commands.Goal.UpdateGoal;
using BudgetService.Application.Handlers.Queries.Goal.GetGoalById;
using BudgetService.Application.Handlers.Queries.Goal.GetGoalByUserId;
using BudgetService.Application.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BudgetService.API.Controllers;

[ApiController]
[Route("goals")]
public class GoalController(IMediator mediator): ControllerBase
{
    [HttpGet("{goalId:Guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid idGoal, CancellationToken cancellationToken)
    {
        var goal = await mediator.Send(new GetGoalByIdQuery(idGoal), cancellationToken);

        return Ok(goal);
    }
    
    [HttpGet("/users/{userId:Guid}/goals")]
    public async Task<IActionResult> GetByUserId([FromRoute] Guid userId, CancellationToken cancellationToken)
    {
        var goals = await mediator.Send(new GetGoalsByUserIdQuery(userId), cancellationToken);

        return Ok(goals);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateGoalCommand request, CancellationToken cancellationToken)
    {
        var goal = await mediator.Send(request, cancellationToken);

        return Ok(goal);
    }
    
    [HttpPut("{goalId:Guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid idGoal, [FromBody] UpdateGoalDto dto, CancellationToken cancellationToken)
    {
        await mediator.Send(new UpdateGoalCommand(idGoal, dto), cancellationToken);
    
        return NoContent();
    }
    
    [HttpDelete("{goalId:Guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid idGoal, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteGoalCommand(idGoal), cancellationToken);

        return NoContent();
    }
}