using Arch.EntityFrameworkCore.UnitOfWork;
using FluentValidation.AspNetCore;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using WebApi.Configurations;
using WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<DataContext>(options => options
    .UseInMemoryDatabase("chat-room-db"))
    .AddUnitOfWork<DataContext>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerConfig();
builder.Services.ResolveDependencies();
builder.Services.RegisterMapper();
builder.Services.AddApiConfig(builder.Configuration);
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<DataContext>();

// Populating the EF InMemory Database
context!.SeedData();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ExceptionMiddleware>();

app.Run();
