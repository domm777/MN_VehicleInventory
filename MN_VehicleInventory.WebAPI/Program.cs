using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.SqlServer;
using MN_VehicleInventory.Application.Interfaces;
using MN_VehicleInventory.Application.Services;
using MN_VehicleInventory.Infrastructure.Persistence;
using MN_VehicleInventory.Infrastructure.Repositories; // Add the following using directive to enable UseSqlServer extension method

var builder = WebApplication.CreateBuilder(args);

// Registering the data base context with the dependency injection container
builder.Services.AddDbContext<MN_InventoryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Repositories and Services (Dependency Injection)
builder.Services.AddScoped<MN_IVehicleRepository, MN_VehicleRepository>();
builder.Services.AddScoped<MN_IVehicleService, MN_VehicleService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();