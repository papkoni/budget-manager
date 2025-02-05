using Microsoft.AspNetCore.Mvc;
using UserService.Application.DTO;
using UserService.Application.Interfaces.Services;

namespace UserService.API.Controllers.Http;

[ApiController]
[Route("auth")]
public class AuthController: ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService )
    {
        _authService = authService;
    }
    
    [HttpPost("/register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
    {
        var context = HttpContext;

        var tokens  = await _authService.RegisterAsync(
            request.Name, 
            request.Password,
            request.Email);

        context.Response.Cookies.Append("secretCookie", tokens.RefreshToken);

        return Ok( tokens.AccessToken );
    }
    
    [HttpPost("/logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("secretCookie"); 

        return Ok();
    }

    
    [HttpPost("/login")]
    public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
    {
        var context = HttpContext;

        var tokens = await _authService.LoginAsync(request.Email, request.Password);

        context.Response.Cookies.Append("secretCookie", tokens.RefreshToken);

        return Ok( tokens.AccessToken );
    }
    
    [HttpPost("/refresh")]
    public async Task<ActionResult<string>> RefreshTokens()
    {
        var refreshToken = Request.Cookies["secretCookie"];
        var tokens = await _authService.RefreshTokensAsync(refreshToken);

        return Ok( tokens.AccessToken );
    }
}