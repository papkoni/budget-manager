using System.Security.Claims;
using Bogus;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using UserService.API.Controllers.Http;
using UserService.Application.DTO;
using UserService.Application.Interfaces.Services;

namespace UserService.Tests.User;


public class UserControllerTests
{
    private readonly Mock<IUserService> _userServiceMock;
    private readonly UserController _controller;
    private readonly Faker _faker = new();

    public UserControllerTests()
    {
        _userServiceMock = new Mock<IUserService>();
        _controller = new UserController(_userServiceMock.Object)
        {
            ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext()
            }
        };
    }

    [Fact]
    public async Task GetAllUsers_ShouldReturnUsersList()
    {
        // Arrange
        var users = new List<UsersResponse>
        {
            new UsersResponse(Guid.NewGuid(), _faker.Internet.Email(), _faker.Name.FirstName()),
            new UsersResponse(Guid.NewGuid(), _faker.Internet.Email(), _faker.Name.FirstName())
        };

        _userServiceMock.Setup(s => s.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(users);

        // Act
        var result = await _controller.GetAllUsers(CancellationToken.None);

        // Assert
        var okResult = result.Result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult.StatusCode.Should().Be(StatusCodes.Status200OK);

        var returnedUsers = okResult.Value as List<UsersResponse>;
        returnedUsers.Should().NotBeNull();
        returnedUsers.Should().HaveCount(2);
    }

    [Fact]
    public async Task GetUserById_ShouldReturnUser_WhenUserExists()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var userResponse = new UserResponse(userId, _faker.Internet.Email(), _faker.Name.FirstName(), "User");

        var userClaims = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim(ClaimTypes.Role, "User")
        }, "mock"));

        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = userClaims }
        };

        _userServiceMock.Setup(s => s.GetUserByIdAsync(userId, userId.ToString(), "User", It.IsAny<CancellationToken>()))
            .ReturnsAsync(userResponse);

        // Act
        var result = await _controller.GetUserById(userId, CancellationToken.None);

        // Assert
        var okResult = result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult.StatusCode.Should().Be(StatusCodes.Status200OK);

        var returnedUser = okResult.Value as UserResponse;
        returnedUser.Should().NotBeNull();
        returnedUser.Id.Should().Be(userId);
        returnedUser.Role.Should().Be("User");
    }
}