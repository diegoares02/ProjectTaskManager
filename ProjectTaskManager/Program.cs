using System.Reflection;
using FluentValidation.AspNetCore;
using TaskManagement.Infrastructure.Data;
using TaskManagement.Infrastructure.Data.Repositories;
using TaskManager.Core.Application.Interfaces;
using TaskManager.Core.Application.Tasks.Handlers;
using TaskManager.Core.Application.Tasks.Validators;
using TaskManager.Core.Domain.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateTaskCommandValidator>());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateTaskHandler).Assembly));

builder.Services.AddSingleton<IDatabaseConnection, DatabaseConnection>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("DefaultConnection");
    return new DatabaseConnection(connectionString);
});
builder.Services.AddScoped<ITaskDependencyValidator, TaskDependencyValidator>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
