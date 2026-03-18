using Maintenance.Repository;
using Maintenance.Service;
//using MN_ApiGateWay.Middleware;
using MN_VehicleInventory.Shared.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddSwaggerGen(c => {
//    // Add API key
//    c.AddSecurityDefinition("ApiKey", new Microsoft.OpenApi.OpenApiSecurityScheme {
//        Description = "API Key needed to access the endpoints. Add X-Api-Key: MY_SECRET_KEY_123",
//        Name = "X-Api-Key",
//        In = Microsoft.OpenApi.ParameterLocation.Header,
//        Type = Microsoft.OpenApi.SecuritySchemeType.ApiKey,
//        Scheme = "ApiKeyScheme"
//    });

//    //c.AddSecurityRequirement(new Microsoft.OpenApi.OpenApiSecurityRequirement
//    //{
//    //    {
//    //        new Microsoft.OpenApi.OpenApiSecurityScheme
//    //        {
//    //            Reference = new Microsoft.OpenApi.OpenApiReference
//    //            {
//    //                Type = Microsoft.OpenApi.ReferenceType.SecurityScheme,
//    //                Id = "ApiKey"
//    //            },
//    //            Scheme = "ApiKeyScheme",
//    //            Name = "X-Api-Key",
//    //            In = Microsoft.OpenApi.Models.ParameterLocation.Header
//    //        },
//    //        new List<string>()
//    //    }
//    //});
//});

var usageCounts = new Dictionary<string, int>();
builder.Services.AddSingleton(usageCounts);

builder.Services.AddScoped<IRepairHistoryService, FakeRepairHistoryService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.Use(async (context, next) => {
    try {
        await next();
    } catch (Exception ex) {
        Console.WriteLine(ex.Message);
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(new {
            error = "ServerError",
            message = "An unexpected error occurred."
        });
    }
});

const string API_KEY = "MY_SECRET_KEY_123";
app.Use(async (context, next) => {
    if (context.Request.Path.StartsWithSegments("/swagger")) {
        await next();
        return;
    }

    if (!context.Request.Headers.TryGetValue("X-Api-Key", out var key) || key != API_KEY) {
        context.Response.StatusCode = 401;
        await context.Response.WriteAsJsonAsync(new {
            error = "Unauthorized",
            message = "Missing or invalid API key."
        });
        return;
    }

    await next();
});

app.UseMiddleware<ApiKeyMiddleware>();
app.UseMiddleware<BlockDirectAccessMiddleware>();

app.MapControllers();

app.Run();
