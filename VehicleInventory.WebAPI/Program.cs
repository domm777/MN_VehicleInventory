using Microsoft.EntityFrameworkCore;
using VehicleInventory.Application.Interfaces;
using VehicleInventory.Application.Services;
using VehicleInventory.Infrastructure.Persistence;
using VehicleInventory.Infrastructure.Repositories;

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