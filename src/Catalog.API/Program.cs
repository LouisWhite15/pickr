using Catalog.API;
using Catalog.API.Apis;
using Catalog.API.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Pickr.AppHost.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddProblemDetails();

builder.AddApplicationServices();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapDefaultEndpoints();

app.MapCatalogApi();

// Migrate the database on app startup
// This will need to be cleaned up later
var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using var scope = scopeFactory.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<CatalogContext>();

await dbContext.Database.MigrateAsync();

app.Run();
