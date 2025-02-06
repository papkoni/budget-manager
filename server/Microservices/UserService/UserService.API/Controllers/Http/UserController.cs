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
    public async Task<ActionResult<List<UsersResponce>>> GetAllUsers()
    {
        var response = await _userService.GetAllAsync();
        
        return Ok(response);
    }
    
    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userRoleClaim = User.FindFirst(ClaimTypes.Role)?.Value;
        if (userIdClaim == null)
        {
            return Unauthorized(new { message = "Unauthorized access" });
        }
        
        bool isOwner = userIdClaim == id.ToString();
        bool isAdmin = userRoleClaim == "Admin";
        if (!isOwner && !isAdmin)
        {
            return Forbid();
        }

        var foundUser = await _userService.GetUserByIdAsync(id);

        return Ok(foundUser);
    }
}