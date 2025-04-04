using System.Security.Claims;
using Bogus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using UserService.API.Controllers.Http;
using UserService.Application.DTO;
using UserService.Application.Exceptions;
using UserService.Application.Interfaces.Services;

namespace UserService.Tests.Auth;

public class AuthControllerTests
{
    private readonly Mock<IAuthService> _authServiceMock;
    private readonly AuthController _controller;
    private readonly Faker _faker = new();

    public AuthControllerTests()
    {
        _authServiceMock = new Mock<IAuthService>();
        _controller = new AuthController(_authServiceMock.Object)
        {
            ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext()
            }
        };
    }

    [Fact]
    public async Task Register_ValidRequest_ReturnsOkWithAccessToken()
    {
        // Arrange
        var request = new RegisterUserRequest(
            _faker.Internet.Email(),
            _faker.Internet.Password(),
            _faker.Name.FirstName()
        );

        var expectedTokens = new TokensDTO(
            _faker.Random.Guid().ToString(),
            _faker.Random.Guid().ToString()
        );
       
        _authServiceMock.Setup(x => x.RegisterAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedTokens);

        // Act
        var result = await _controller.Register(request, CancellationToken.None);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(expectedTokens.AccessToken, okResult.Value);
        
        // Проверка cookie
        var cookie = _controller.HttpContext.Response.GetTypedHeaders().SetCookie.FirstOrDefault(c => c.Name == "secretCookie");
        Assert.NotNull(cookie);
        Assert.Contains(expectedTokens.RefreshToken, cookie.Value.ToString());
    }

    [Fact]
    public async Task Register_ServiceThrowsException_ReturnsBadRequest()
    {
        // Arrange
        var request = new RegisterUserRequest(
            _faker.Internet.Email(),
            _faker.Internet.Password(),
            _faker.Name.FirstName()
        );
        
        _authServiceMock.Setup(x => x.RegisterAsync(request, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new BadRequestException("Test error"));

        // Act
        IActionResult result = null;
        try
        {
            result = await _controller.Register(request, CancellationToken.None);
        }
        catch (BadRequestException ex)
        {
            // Имитируем обработку исключения middleware
            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Detail = ex.Message
            };
            result = new ObjectResult(problemDetails) 
            { 
                StatusCode = StatusCodes.Status400BadRequest 
            };
        }

        // Assert
        var objectResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(StatusCodes.Status400BadRequest, objectResult.StatusCode);
        Assert.IsType<ProblemDetails>(objectResult.Value);
    }
    
    [Fact]
    public async Task Logout_ValidUser_ReturnsOkAndDeletesCookie()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var userClaims = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString())
        }, "mock"));

        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = userClaims }
        };

        _authServiceMock.Setup(s => s.RevokeTokenAsync(userId, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _controller.Logout(CancellationToken.None);

        // Assert
        var okResult = Assert.IsType<OkResult>(result);
    
        // Проверяем, что в заголовках Set-Cookie кука установлена с истёкшим сроком
        var setCookieHeader = _controller.HttpContext.Response.Headers["Set-Cookie"].ToString();
        Assert.Contains("secretCookie=;", setCookieHeader);
        Assert.Contains("expires=Thu, 01 Jan 1970", setCookieHeader);
    }

    [Fact]
    public async Task Login_ValidCredentials_ReturnsOkWithAccessTokenAndSetsCookie()
    {
        // Arrange
        var request = new LoginUserRequest(
            _faker.Internet.Email(),
            _faker.Internet.Password()
        );

        var expectedTokens = new TokensDTO(
            _faker.Random.Guid().ToString(),
            _faker.Random.Guid().ToString()
        );

        _authServiceMock.Setup(s => s.LoginAsync(request.Email, request.Password, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedTokens);

        // Act
        var result = await _controller.Login(request, CancellationToken.None);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(expectedTokens.AccessToken, okResult.Value);
        
        // Проверяем, что refresh token записан в cookie
        var cookie = _controller.HttpContext.Response.GetTypedHeaders().SetCookie.FirstOrDefault(c => c.Name == "secretCookie");
        Assert.NotNull(cookie);
        Assert.Contains(expectedTokens.RefreshToken, cookie.Value.ToString());
    }

    [Fact]
    public async Task RefreshTokens_ValidRefreshToken_ReturnsNewAccessToken()
    {
        // Arrange
        var oldRefreshToken = _faker.Random.Guid().ToString();
        var newTokens = new TokensDTO(
            _faker.Random.Guid().ToString(),
            _faker.Random.Guid().ToString()
        );

        _controller.HttpContext.Request.Headers["Cookie"] = $"secretCookie={oldRefreshToken}";


        _authServiceMock.Setup(s => s.RefreshTokensAsync(oldRefreshToken, It.IsAny<CancellationToken>()))
            .ReturnsAsync(newTokens);

        // Act
        var result = await _controller.RefreshTokens(CancellationToken.None);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(newTokens.AccessToken, okResult.Value);

        // Проверяем, что новый refresh token записан в cookie
        var cookie = _controller.HttpContext.Response.GetTypedHeaders().SetCookie.FirstOrDefault(c => c.Name == "secretCookie");
        Assert.NotNull(cookie);
        Assert.Contains(newTokens.RefreshToken, cookie.Value.ToString());
    }
}