using Bogus;
using FluentAssertions;
using FluentValidation;
using Moq;
using UserService.Application.DTO;
using UserService.Application.Exceptions;
using UserService.Application.Interfaces.Auth;
using UserService.Application.Services;
using UserService.Persistence.Enums;
using UserService.Persistence.Interfaces;
using UserService.Persistence.Interfaces.Repositories;
using UserService.Persistence.Models;

namespace UserService.Tests.Auth;

public class AuthServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IJwtProvider> _jwtProviderMock;
    private readonly Mock<IPasswordHasher> _passwordHasherMock;
    private readonly Mock<IRefreshTokenRepository> _refreshTokenRepositoryMock;
    private readonly Mock<IUserServiceDbContext> _userServiceDbContextMock;
    private readonly Mock<IValidator<RegisterUserRequest>> _validatorMock;

    private readonly AuthService _authService;
    
    private readonly Faker _faker = new();
    
    public AuthServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _jwtProviderMock = new Mock<IJwtProvider>();
        _passwordHasherMock = new Mock<IPasswordHasher>();
        _refreshTokenRepositoryMock = new Mock<IRefreshTokenRepository>();
        _userServiceDbContextMock = new Mock<IUserServiceDbContext>();
        _validatorMock = new Mock<IValidator<RegisterUserRequest>>();

        _authService = new AuthService(
            _userRepositoryMock.Object,
            _passwordHasherMock.Object,
            _jwtProviderMock.Object,
            _refreshTokenRepositoryMock.Object,
            _userServiceDbContextMock.Object,
            _validatorMock.Object
        );
    }
    
    [Fact]
    public async Task RegisterAsync_ShouldRegisterUser_WhenDataIsValid()
    {
        // Arrange
        var request = new RegisterUserRequest
        (
            _faker.Internet.Email(),
            _faker.Internet.Password(),
            _faker.Name.FirstName()
        );
        
        var hashedPassword = _faker.Random.AlphaNumeric(10);
        var tokens = new TokensDTO(_faker.Random.Guid().ToString(), _faker.Random.Guid().ToString());

        _validatorMock.Setup(v => v.Validate(request)).Returns(new FluentValidation.Results.ValidationResult());

        _userRepositoryMock.Setup(r => r.GetByEmailAsync(request.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync((UserModel)null);

        _passwordHasherMock.Setup(h => h.Generate(request.Password)).Returns(hashedPassword);

        _jwtProviderMock.Setup(j => j.GenerateTokens(It.IsAny<UserModel>())).Returns(tokens);
        _jwtProviderMock.Setup(j => j.GetRefreshTokenExpirationMinutes()).Returns(30);

        _userRepositoryMock.Setup(r => r.AddAsync(It.IsAny<UserModel>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        _refreshTokenRepositoryMock.Setup(r => r.AddAsync(It.IsAny<RefreshTokenModel>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        _userServiceDbContextMock.Setup(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        var result = await _authService.RegisterAsync(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.AccessToken.Should().NotBeNullOrWhiteSpace();
        result.RefreshToken.Should().NotBeNullOrWhiteSpace();

        _userRepositoryMock.Verify(r => r.AddAsync(It.IsAny<UserModel>(), It.IsAny<CancellationToken>()), Times.Once);
        _refreshTokenRepositoryMock.Verify(r => r.AddAsync(It.IsAny<RefreshTokenModel>(), It.IsAny<CancellationToken>()), Times.Once);
        _userServiceDbContextMock.Verify(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
    
    [Fact]
    public async Task RegisterAsync_ShouldThrowBadRequestException_WhenUserAlreadyExists()
    {
        // Arrange
        var request = new RegisterUserRequest
        (
            _faker.Internet.Email(),
            _faker.Internet.Password(),
            _faker.Name.FirstName()
        );
        
        var existingUser = new UserModel(Guid.NewGuid(),_faker.Name.FirstName(), _faker.Internet.Password(), request.Email, Role.User);

        _validatorMock.Setup(v => v.Validate(request)).Returns(new FluentValidation.Results.ValidationResult());
        
        _userRepositoryMock.Setup(r => r.GetByEmailAsync(request.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingUser);

        // Act
        var act = async () => await _authService.RegisterAsync(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<BadRequestException>().WithMessage($"A user with the email '{request.Email}' already exists");
    }
    
    [Fact]
    public async Task LoginAsync_ShouldReturnTokens_WhenCredentialsAreValid()
    {
        // Arrange
        var email = _faker.Internet.Email();
        var password = _faker.Internet.Password();
        var hashedPassword = _faker.Random.AlphaNumeric(10);
        var tokens = new TokensDTO(_faker.Random.Guid().ToString(), _faker.Random.Guid().ToString());

        var user = new UserModel(Guid.NewGuid(), password, hashedPassword, email, Role.User)
        {
            RefreshToken = new RefreshTokenModel(Guid.NewGuid(), tokens.RefreshToken, 30, Guid.NewGuid())
        };

        _userRepositoryMock.Setup(r => r.GetByEmailAsync(email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        _passwordHasherMock.Setup(h => h.Verify(password, user.PasswordHash)).Returns(true);

        _jwtProviderMock.Setup(j => j.GenerateTokens(user)).Returns(tokens);
        _jwtProviderMock.Setup(j => j.GetRefreshTokenExpirationMinutes()).Returns(30);

        _userServiceDbContextMock.Setup(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        var result = await _authService.LoginAsync(email, password, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.AccessToken.Should().NotBeNullOrWhiteSpace();
        result.RefreshToken.Should().NotBeNullOrWhiteSpace();

        _userServiceDbContextMock.Verify(db => db.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task LoginAsync_ShouldThrowNotFoundException_WhenUserDoesNotExist()
    {
        // Arrange
        var email = _faker.Internet.Email();
        var password = _faker.Internet.Password();

        _userRepositoryMock.Setup(r => r.GetByEmailAsync(email, It.IsAny<CancellationToken>()))
            .ReturnsAsync((UserModel)null); // Пользователь не найден

        // Act
        var act = async () => await _authService.LoginAsync(email, password, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage("User not found");
    }

    [Fact]
    public async Task LoginAsync_ShouldThrowUnauthorizedException_WhenPasswordIsIncorrect()
    {
        // Arrange
        var email = _faker.Internet.Email();
        var password = _faker.Internet.Password();
        var hashedPassword = _faker.Random.AlphaNumeric(10);

        var user = new UserModel(Guid.NewGuid(), password, hashedPassword, email, Role.User);

        _userRepositoryMock.Setup(r => r.GetByEmailAsync(email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        _passwordHasherMock.Setup(h => h.Verify(password, user.PasswordHash)).Returns(false); // Пароль неверный

        // Act
        var act = async () => await _authService.LoginAsync(email, password, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<UnauthorizedException>()
            .WithMessage("Invalid password or email");
    }
    
    [Fact]
    public async Task RefreshTokensAsync_ShouldThrowInvalidTokenException_WhenRefreshTokenIsInvalid()
    {
        // Arrange
        var refreshToken = _faker.Random.Guid().ToString();

        _jwtProviderMock.Setup(j => j.ValidateRefreshToken(refreshToken)).Returns(false); // Токен недействителен

        // Act
        var act = async () => await _authService.RefreshTokensAsync(refreshToken, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<InvalidTokenException>()
            .WithMessage("Token was expire");
    }

    [Fact]
    public async Task RefreshTokensAsync_ShouldThrowNotFoundException_WhenUserDoesNotExist()
    {
        // Arrange
        var refreshToken = _faker.Random.Guid().ToString();
        var userId = Guid.NewGuid();

        _jwtProviderMock.Setup(j => j.ValidateRefreshToken(refreshToken)).Returns(true);
        _jwtProviderMock.Setup(j => j.GetUserIdFromRefreshToken(refreshToken)).Returns(userId);

        _userRepositoryMock.Setup(r => r.GetByIdAsync(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((UserModel)null); // Пользователь не найден

        // Act
        var act = async () => await _authService.RefreshTokensAsync(refreshToken, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage("User not found");
    }
}