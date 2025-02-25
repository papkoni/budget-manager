using BudgetService.API.Extensions;
using BudgetService.API.Middlewares;
using BudgetService.Application.Extensions;
using BudgetService.Application.Handlers.Commands.Budget.CreateBudget;
using BudgetService.Application.Validators;
using BudgetService.Domain.Entities;
using BudgetService.Persistence.Extensions;
using FluentValidation;
using MapsterMapper;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor(); 

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApi(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ApplyMigrations();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();