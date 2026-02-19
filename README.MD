# Vehicle Inventory Microservice

## Overview
In this Assinment I worked on design and implement a Vehicle Inventory microservice using Clean Architecture and Domain-Driven Design (DDD) principles. The service will expose a RESTful CRUD API using ASP.NET Core Web API, Entity Framework Core, and Swagger.

## Architecture
The solution is divided into four strictly separated layers:

1.  Domain Layer: `MN_VehicleInventory.Domain` Contains the the entity and core business logic.
        Rules: No dependencies on frameworks or databases.
        Key Logic: Validates state transitions (e.g. MN_Vehicle cannot be rented if it is under service).
        Project References: None

2.  Application Layer: `MN_VehicleInventory.Application` Orchestrates use cases (CRUD). 
        Definds interfaces for persistence and communications using DTOs
        Components: `MN_IVehicleService`, `MN_IVehicleRepository`, DTOs.
        Role: Connects the API to the Domain without exposing database details.
        Project References: Domain
        
3.  Infrastructure Layer: `MN_VehicleInventory.Infrastructure` Handles data persistence.
        Entity Framework Core, SQL Server (LocalDB).
        Role: Implements repositories and manages database migrations.
        Project References: Application

4.  WebAPI Layer: Exposes RESTful endpoints. 
        Configures dependency injection and exposses REST endpoints using `MN_VehiclesController.cs`
        ASP.NET Core Controllers, Swagger UI.
        Project References: Application & Infrastructure

## Business Rules Implemented
    Encapsulation: MN_Vehicle properties are read-only from the outside; methods like `MarkRented()` must be used.
    Validation:
    Cannot rent a MN_Vehicle that is already rented.
    Cannot rent a MN_Vehicle that is reserved.
    Cannot rent a MN_Vehicle that is under service.
    Reserved MN_Vehicles must be released before being made available.

## How to Run
1.  Prerequisites: .NET 8 SDK, SQL Server LocalDB.
2.  Database Setup:
    The application uses EF Core migrations. The database will be created automatically if you run the update command:
```bash
    dotnet ef database update --project MN_VehicleInventory.Infrastructure --startup-project MN_VehicleInventory.WebAPI
```
3.  Run the API:
```bash
    dotnet run --project MN_VehicleInventory.WebAPI
```
4.  Explore:
Open your browser to the URL shown in the console under the green text (usually `http://localhost:5056/swagger/index.html`) to use the Swagger UI.

# DB Setup
```Bash
    # 1. Clean previous migrations (if any)
    # Delete folders in MN_VehicleInventory.Infrastructure/Migrations

    # 2. Add Migration
    dotnet ef migrations add InitialCreate --project MN_VehicleInventory.Infrastructure --startup-project MN_VehicleInventory.WebAPI

    # 3. Update Database
    dotnet ef database update --project MN_VehicleInventory.Infrastructure --startup-project MN_VehicleInventory.WebAPI
```

## Known Limitations
    Authentication is currently out of scope.
    Database is LocalDB (not production-ready). Manual input and not seeded in the 

## Shortcuts to make the project structure & setup
# Create Solution
dotnet new sln -n MN_VehicleInventory

# Create Layers with .NET 8
dotnet new classlib -n MN_VehicleInventory.Domain -f net8.0
dotnet new classlib -n MN_VehicleInventory.Application -f net8.0
dotnet new classlib -n MN_VehicleInventory.Infrastructure -f net8.0
dotnet new webapi -n MN_VehicleInventory.WebAPI -f net8.0

# Add Projects to Solution
dotnet sln add MN_VehicleInventory.Domain
dotnet sln add MN_VehicleInventory.Application
dotnet sln add MN_VehicleInventory.Infrastructure
dotnet sln add MN_VehicleInventory.WebAPI

# Establish References (The Dependency Flow)
dotnet add MN_VehicleInventory.Application reference MN_VehicleInventory.Domain
dotnet add MN_VehicleInventory.Infrastructure reference MN_VehicleInventory.Application
dotnet add MN_VehicleInventory.WebAPI reference MN_VehicleInventory.Application
dotnet add MN_VehicleInventory.WebAPI reference MN_VehicleInventory.Infrastructure

## Shortcuts to install packages on WebAPI & Infrastructure
# Install packages on MN_VehicleInventory.WebAPI
dotnet add MN_VehicleInventory.WebAPI package Microsoft.EntityFrameworkCore.Design --version 8.0.24
dotnet add MN_VehicleInventory.WebAPI package Swashbuckle.AspNetCore --version 6.6.2

# Install packages on MN_VehicleInventory.Infrastructure
dotnet add MN_VehicleInventory.Infrastructure package Microsoft.EntityFrameworkCore --version 8.0.24
dotnet add MN_VehicleInventory.Infrastructure package Microsoft.EntityFrameworkCore.Design --version 8.0.24
dotnet add MN_VehicleInventory.Infrastructure package Microsoft.EntityFrameworkCore.SqlServer --version 8.0.24
dotnet add MN_VehicleInventory.Infrastructure package Microsoft.EntityFrameworkCore.Tools --version 8.0.24