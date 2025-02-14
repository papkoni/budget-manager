using System.Security.Claims;
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
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest request, CancellationToken cancellationToken)
    {
        var context = HttpContext;

        var tokens  = await _authService.RegisterAsync(request, cancellationToken);

        context.Response.Cookies.Append("secretCookie", tokens.RefreshToken);

        return Ok(tokens.AccessToken);
    }
    
    [HttpPost("/logout")]
    public async Task<IActionResult> Logout(CancellationToken cancellationToken)
    {
        Response.Cookies.Delete("secretCookie");
        
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        await _authService.RevokeTokenAsync(Guid.Parse(userIdClaim), cancellationToken);
        
        return Ok();
    }

    [HttpPost("/login")]
    public async Task<IActionResult> Login([FromBody] LoginUserRequest request, CancellationToken cancellationToken)
    {
        var context = HttpContext;

        var tokens = await _authService.LoginAsync(request.Email, request.Password, cancellationToken);

        context.Response.Cookies.Append("secretCookie", tokens.RefreshToken);

        return Ok(tokens.AccessToken);
    }
    
    [HttpPost("/refresh")]
    public async Task<ActionResult<string>> RefreshTokens(CancellationToken cancellationToken)
    {
        var refreshToken = Request.Cookies["secretCookie"];
        
        var tokens = await _authService.RefreshTokensAsync(refreshToken, cancellationToken);

        Response.Cookies.Append("secretCookie", tokens.RefreshToken);
        
        return Ok(tokens.AccessToken);
    }
}