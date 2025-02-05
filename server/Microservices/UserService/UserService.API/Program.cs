using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using UserService.API.Extensions;
using UserService.API.Middleware;

using UserService.Application.Interfaces.Services;
using UserService.Application.Services;
using UserService.Application.Infrastructure.Authentication;
using UserService.Persistence;
using UserService.Persistence.Interfaces;
using UserService.Persistence.Interfaces.Auth;
using UserService.Persistence.Interfaces.Repositories;
using UserService.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor(); 


builder.Services.AddControllers();

builder.Services.AddApiAuthentication(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<IUserServiceDbContext,UserServiceDbContext>(
    options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(UserServiceDbContext)));
    }
);

var services = builder.Services;


services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));
services.AddScoped<IJwtProvider, JwtProvider>();
services.AddScoped<IPasswordHasher, PasswordHasher>();
services.AddScoped<IUserRepository, UserRepository>();
services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
services.AddScoped<IAuthService, AuthService>();
services.AddScoped<IUserService, UserService.Application.Services.UserService>();

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Введите 'Bearer {токен}' (без кавычек)",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();

}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();

