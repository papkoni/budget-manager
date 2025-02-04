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
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null)
        {
            return NotFound(new { message = "User not found" });
        }
        
        return Ok(user);
    }
}