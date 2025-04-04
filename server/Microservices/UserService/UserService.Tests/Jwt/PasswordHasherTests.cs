using FluentAssertions;
using Bogus;
using UserService.Application.Infrastructure.Authentication;

namespace UserService.Tests.Jwt;

public class PasswordHasherTests
{
    private readonly PasswordHasher _passwordHasher;
    private readonly Faker _faker = new();

    public PasswordHasherTests()
    {
        _passwordHasher = new PasswordHasher();
    }

    [Fact]
    public void Generate_ShouldReturnHashedPassword()
    {
        // Arrange
        var password = _faker.Internet.Password();

        // Act
        var hashedPassword = _passwordHasher.Generate(password);

        // Assert
        hashedPassword.Should().NotBeNullOrWhiteSpace();
        hashedPassword.Should().NotBe(password);
        hashedPassword.Should().StartWith("$2"); // Проверяем, что строка хеширована BCrypt
    }

    [Fact]
    public void Verify_ShouldReturnTrue_WhenPasswordIsCorrect()
    {
        // Arrange
        var password = _faker.Internet.Password();
        var hashedPassword = _passwordHasher.Generate(password);

        // Act
        var result = _passwordHasher.Verify(password, hashedPassword);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void Verify_ShouldReturnFalse_WhenPasswordIsIncorrect()
    {
        // Arrange
        var correctPassword = _faker.Internet.Password();
        var incorrectPassword = _faker.Internet.Password();
        var hashedPassword = _passwordHasher.Generate(correctPassword);

        // Act
        var result = _passwordHasher.Verify(incorrectPassword, hashedPassword);

        // Assert
        result.Should().BeFalse();
    }
}
