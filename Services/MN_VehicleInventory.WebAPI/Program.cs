using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.SqlServer;
//using MN_ApiGateWay.Middleware;
using MN_VehicleInventory.Application.Interfaces;
using MN_VehicleInventory.Application.Services;
using MN_VehicleInventory.Infrastructure.Persistence;
using MN_VehicleInventory.Infrastructure.Repositories;

// Adding the commen class lib
using MN_VehicleInventory.Shared;
using MN_VehicleInventory.Shared.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MN_InventoryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Dependency Injection
builder.Services.AddScoped<MN_IVehicleRepository, MN_VehicleRepository>();
builder.Services.AddScoped<MN_IVehicleService, MN_VehicleService>();

// Add controllers and Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Adding middleware pipline using DRY method talked in class in every microservice we build in the future instead of repeating logic.
app.ConfigureGlobalExceptionHandler();


// Configure the HTTP request pipeline. We are in development environment, so we want to use Swagger for API documentation and testing.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<BlockDirectAccessMiddleware>();

app.MapControllers();

app.Run();