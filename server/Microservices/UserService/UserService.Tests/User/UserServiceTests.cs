using Bogus;
using FluentAssertions;
using Moq;
using UserService.Application.DTO;
using UserService.Application.Exceptions;
using UserService.Persistence.Enums;
using UserService.Persistence.Interfaces.Repositories;
using UserService.Persistence.Models;

namespace UserService.Tests.User;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Application.Services.UserService _userService;
    private readonly Faker _faker = new();

    public UserServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _userService = new Application.Services.UserService(_userRepositoryMock.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnUsersList()
    {
        // Arrange
        var users = new List<UserModel>
        {
            new UserModel(Guid.NewGuid(), _faker.Name.FirstName(), _faker.Internet.Password(), _faker.Internet.Email(), Role.User),
            new UserModel(Guid.NewGuid(), _faker.Name.FirstName(), _faker.Internet.Password(), _faker.Internet.Email(), Role.Admin)
        };

        _userRepositoryMock.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(users);

        // Act
        var result = await _userService.GetAllAsync(CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        result.Should().AllBeOfType<UsersResponse>();
    }

    [Fact]
    public async Task GetUserByIdAsync_ShouldReturnUser_WhenUserExistsAndHasAccess()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var userModel = new UserModel(userId, _faker.Name.FirstName(), _faker.Internet.Password(), _faker.Internet.Email(), Role.User);

        _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(userModel);

        // Act
        var result = await _userService.GetUserByIdAsync(userId, userId.ToString(), Role.User.ToString(), CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(userId);
        result.Email.Should().Be(userModel.Email);
        result.Role.Should().Be(Role.User.ToString());
    }

    [Fact]
    public async Task GetUserByIdAsync_ShouldThrowUnauthorizedException_WhenUserIdClaimIsNull()
    {
        // Arrange
        var userId = Guid.NewGuid();

        // Act
        Func<Task> act = async () => await _userService.GetUserByIdAsync(userId, null, Role.User.ToString(), CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<UnauthorizedException>()
            .WithMessage("Unauthorized access");
    }

    [Fact]
    public async Task GetUserByIdAsync_ShouldThrowForbiddenException_WhenUserIsNotOwnerOrAdmin()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var otherUserId = Guid.NewGuid();

        // Act
        Func<Task> act = async () => await _userService.GetUserByIdAsync(userId, otherUserId.ToString(), Role.User.ToString(), CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ForbiddenException>()
            .WithMessage("Access is forbidden.");
    }

    [Fact]
    public async Task GetUserByIdAsync_ShouldThrowNotFoundException_WhenUserDoesNotExist()
    {
        // Arrange
        var userId = Guid.NewGuid();

        _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((UserModel)null);

        // Act
        Func<Task> act = async () => await _userService.GetUserByIdAsync(userId, userId.ToString(), Role.User.ToString(), CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage("User not found");
    }
}

