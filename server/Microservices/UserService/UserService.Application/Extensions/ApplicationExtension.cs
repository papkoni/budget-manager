using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserService.Application.DTO;
using UserService.Application.Infrastructure.Authentication;
using UserService.Application.Interfaces.Auth;
using UserService.Application.Interfaces.Services;
using UserService.Application.Mapping;
using UserService.Application.Services;
using UserService.Application.Validators;

namespace UserService.Application.Extensions;

public static class ApplicationExtension
{
    public static void AddApplication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        MappingConfig.Configure();

        
        services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService.Application.Services.UserService>();
        services.AddScoped<IValidator<RegisterUserRequest>, RegisterUserRequestValidator>(); 
    }
}