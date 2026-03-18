using Microsoft.EntityFrameworkCore;
using Week3Microservice.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient("MaintenanceApi", (sp, client) => {
    var config = sp.GetRequiredService<IConfiguration>();
    client.BaseAddress = new Uri(config["MaintenanceApi:BaseUrl"]!);
});

builder.Services.AddDbContext<CustmerProfileContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Maintenance}/{action=History}/{id?}");

app.Run();