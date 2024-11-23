using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

// Infrastructure
// Application
// API

builder.Services.AddApplicationServices()
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices();

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseApiServices();

if (app.Environment.IsDevelopment())
    await app.InitializeDatabaseAsync();

app.Run();
