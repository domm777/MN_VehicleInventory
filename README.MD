# Vehicle Inventory Microservice

## Overview
In this Assinment I worked on design and implement a Vehicle Inventory microservice using Clean Architecture and Domain-Driven Design (DDD) principles. The service will expose a RESTful CRUD API using ASP.NET Core Web API, Entity Framework Core, and Swagger.

## Architecture
The solution is divided into four strictly separated layers:

1.  Domain Layer: Contains the `MN_Vehicle` entity and business logic.
    Rules: No dependencies on frameworks or databases.
    Key Logic: Validates state transitions (e.g., a MN_Vehicle cannot be rented if it is under service).
2.  Application Layer: Orchestrates use cases.
    Components: `MN_IVehicleService`, `MN_IVehicleRepository`, DTOs.
    Role: Connects the API to the Domain without exposing database details.
3.  Infrastructure Layer: Handles data persistence.
    Entity Framework Core, SQL Server (LocalDB).
    Role: Implements repositories and manages database migrations.
4.  WebAPI Layer: Exposes RESTful endpoints.
    ASP.NET Core Controllers, Swagger UI.

## Business Rules Implemented
    Encapsulation: MN_Vehicle properties are read-only from the outside; methods like `MarkRented()` must be used.
    Validation:
    Cannot rent a MN_Vehicle that is already rented.
    Cannot rent a MN_Vehicle that is reserved.
    Cannot rent a MN_Vehicle that is under service.
    Reserved MN_Vehicles must be released before being made available.

## How to Run
1.  Prerequisites: .NET 9 SDK, SQL Server LocalDB.
2.  Database Setup:
The application uses EF Core migrations. The database will be created automatically if you run the update command:
```bash
dotnet ef database update --project VehicleInventory.Infrastructure --startup-project VehicleInventory.WebAPI
```
3.  Run the API:
```bash
dotnet run --project VehicleInventory.WebAPI
```
4.  Explore:
Open your browser to the URL shown in the console (usually `http://localhost:5078/swagger`) to use the Swagger UI.

## Known Limitations
    Authentication is currently out of scope.
    Database is LocalDB (not production-ready).