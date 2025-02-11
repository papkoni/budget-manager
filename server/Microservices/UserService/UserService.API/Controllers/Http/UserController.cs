using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.DTO;
using UserService.Application.Interfaces.Services;

namespace UserService.API.Controllers.Http;

[ApiController]
[Route("users")]
public class UserController: ControllerBase
{
    private readonly IUserService _userService;
    
    public UserController(IUserService userService )
    {
        _userService = userService;
    }
    
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<UsersResponse>>> GetAllUsers()
    {
        var response = await _userService.GetAllAsync();
        
        return Ok(response);
    }
    
    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById([FromRoute] Guid id)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userRoleClaim = User.FindFirst(ClaimTypes.Role)?.Value;
       
        var user = await _userService.GetUserByIdAsync(id, userIdClaim, userRoleClaim);

        return Ok(user);
    }
}