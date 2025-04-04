using Xunit;
using Moq;
using Microsoft.Extensions.Options;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Bogus;
using UserService.Application.Exceptions;
using UserService.Application.Infrastructure.Authentication;
using UserService.Persistence.Enums;
using UserService.Persistence.Models;

namespace UserService.Tests.Jwt;

public class JwtProviderTests
{
    private readonly Mock<IOptions<JwtOptions>> _optionsMock;
    private readonly JwtProvider _jwtProvider;
    private readonly JwtOptions _jwtOptions;
    private readonly Faker _faker = new();

    public JwtProviderTests()
    {
        // Подготовка mock для JwtOptions
        _jwtOptions = new JwtOptions
        {
            SecretKey = "secretsecretsecretsecretsecretsecretsecretsecret",
            AccessTokenExpiresMinutes = 30,
            RefreshTokenExpiresMinutes = 60
        };

        _optionsMock = new Mock<IOptions<JwtOptions>>();
        _optionsMock.Setup(o => o.Value).Returns(_jwtOptions);

        _jwtProvider = new JwtProvider(_optionsMock.Object);
    }

    [Fact]
    public void GenerateTokens_ShouldReturnValidTokens()
    {
        // Arrange
        var user = new UserModel(
            Guid.NewGuid(), 
            _faker.Name.FullName(), 
            _faker.Internet.Password(), 
            _faker.Internet.Email(), 
            Role.User);

        // Act
        var tokens = _jwtProvider.GenerateTokens(user);

        // Assert
        tokens.Should().NotBeNull();
        tokens.AccessToken.Should().NotBeNullOrWhiteSpace();
        tokens.RefreshToken.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public void GenerateAccessToken_ShouldReturnValidAccessToken()
    {
        // Arrange
        var user = new UserModel(
            Guid.NewGuid(), 
            _faker.Name.FullName(), 
            _faker.Internet.Password(), 
            _faker.Internet.Email(), 
            Role.User);

        // Act
        var accessToken = _jwtProvider.GenerateAccessToken(user);

        // Assert
        accessToken.Should().NotBeNullOrWhiteSpace();
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var jsonToken = tokenHandler.ReadToken(accessToken) as JwtSecurityToken;

        jsonToken.Should().NotBeNull();
        // Используем "nameid", так как при сериализации используется короткое имя для ClaimTypes.NameIdentifier
        jsonToken?.Claims.Should().Contain(c => c.Type == "nameid" && c.Value == user.Id.ToString());
    }

    [Fact]
    public void GenerateRefreshToken_ShouldReturnValidRefreshToken()
    {
        // Arrange
        var userId = Guid.NewGuid();

        // Act
        var refreshToken = _jwtProvider.GenerateRefreshToken(userId);

        // Assert
        refreshToken.Should().NotBeNullOrWhiteSpace();
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var jsonToken = tokenHandler.ReadToken(refreshToken) as JwtSecurityToken;

        jsonToken.Should().NotBeNull();
        // Используем "nameid" для проверки идентификатора
        jsonToken?.Claims.Should().Contain(c => c.Type == "nameid" && c.Value == userId.ToString());
    }

    [Fact]
    public void GetRefreshTokenExpirationMinutes_ShouldReturnCorrectExpirationTime()
    {
        // Act
        var expirationMinutes = _jwtProvider.GetRefreshTokenExpirationMinutes();

        // Assert
        expirationMinutes.Should().Be(_jwtOptions.RefreshTokenExpiresMinutes);
    }

    [Fact]
    public void ValidateRefreshToken_ShouldReturnTrue_WhenTokenIsValid()
    {
        // Arrange
        var refreshToken = _jwtProvider.GenerateRefreshToken(Guid.NewGuid());

        // Act
        var result = _jwtProvider.ValidateRefreshToken(refreshToken);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void GetUserIdFromRefreshToken_ShouldReturnValidUserId_WhenTokenIsValid()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var refreshToken = _jwtProvider.GenerateRefreshToken(userId);

        // Act
        var result = _jwtProvider.GetUserIdFromRefreshToken(refreshToken);

        // Assert
        result.Should().Be(userId);
    }
}