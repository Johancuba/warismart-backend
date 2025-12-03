// Inventory Bounded Context
using WariSmart.API.Inventory.Application.Internal.CommandServices;
using WariSmart.API.Inventory.Application.Internal.QueryServices;
using WariSmart.API.Inventory.Domain.Repositories;
using WariSmart.API.Inventory.Domain.Services;
using WariSmart.API.Inventory.Infrastructure.Persistence.EFC.Repositories;

// Sales Bounded Context
using WariSmart.API.Sales.Application.Internal.CommandServices;
using WariSmart.API.Sales.Application.Internal.QueryServices;
using WariSmart.API.Sales.Domain.Repositories;
using WariSmart.API.Sales.Domain.Services;
using WariSmart.API.Sales.Infrastructure.Persistence.EFC.Repositories;

// IAM Bounded Context
using WariSmart.API.IAM.Application.Internal.CommandServices;
using WariSmart.API.IAM.Application.Internal.QueryServices;
using WariSmart.API.IAM.Domain.Repositories;
using WariSmart.API.IAM.Domain.Services;
using WariSmart.API.IAM.Infrastructure.Persistence.EFC.Repositories;

// Reports Bounded Context
using WariSmart.API.Reports.Application.Internal.QueryServices;
using WariSmart.API.Reports.Domain.Services;
using CatchUpPlatform.API.Shared.Domain.Repositories;
using CatchUpPlatform.API.Shared.Infrastructure.Interfaces.ASP.Configuration;
using CatchUpPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using CatchUpPlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configure Lower Case URLs
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// CORS Configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        builder => builder.WithOrigins(
                            "https://warismart.gustosolutions.tech",
                            "http://localhost:5173"
                          )
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials());
});

// Localization Configuration
builder.Services.AddLocalization();

// Configure Kebab Case Route Naming Convention
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()))
    .AddDataAnnotationsLocalization();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options.EnableAnnotations());

// Add Database Connection

// Configure Database Context and Logging Levels
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<AppDbContext>(
        options =>
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            // Verify Database Connection String
            if (connectionString is null)
                // Stop the application if the connection string is not set.
                throw new Exception("Database connection string is not set.");
            options.UseMySQL(connectionString)
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        });
}
else if (builder.Environment.IsProduction())
{
    builder.Services.AddDbContext<AppDbContext>(options =>
    {
        // Get connection string from configuration (which includes environment variables)
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        
        // If not found in config, try to build from individual environment variables
        if (string.IsNullOrEmpty(connectionString))
        {
            var host = Environment.GetEnvironmentVariable("MYSQLHOST");
            var port = Environment.GetEnvironmentVariable("MYSQLPORT") ?? "3306";
            var database = Environment.GetEnvironmentVariable("MYSQLDATABASE");
            var user = Environment.GetEnvironmentVariable("MYSQLUSER");
            var password = Environment.GetEnvironmentVariable("MYSQLPASSWORD");
            
            if (string.IsNullOrEmpty(host) || string.IsNullOrEmpty(database) || 
                string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
            {
                throw new Exception("Database connection configuration is missing. Please set either ConnectionStrings:DefaultConnection or MYSQL environment variables.");
            }
            
            connectionString = $"Server={host};Port={port};Database={database};User={user};Password={password};CharSet=utf8mb4;SslMode=Preferred;";
        }
        
        Console.WriteLine($"Connecting to database: {connectionString.Split("Password=")[0]}[PASSWORD_HIDDEN]");
        
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Error)
            .EnableDetailedErrors();
    });
}

// Configure Dependency Injection

// Shared Bounded Context Injection Configuration
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Inventory Bounded Context Injection Configuration
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductCommandService, ProductCommandService>();
builder.Services.AddScoped<IProductQueryService, ProductQueryService>();

// Sales Bounded Context Injection Configuration
builder.Services.AddScoped<ISaleRepository, SaleRepository>();
builder.Services.AddScoped<ISaleCommandService, SaleCommandService>();
builder.Services.AddScoped<ISaleQueryService, SaleQueryService>();

// IAM Bounded Context Injection Configuration
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();

// Reports Bounded Context Injection Configuration
builder.Services.AddScoped<IReportQueryService, ReportQueryService>();

var app = builder.Build();

// Verify Database Objects are created
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

// Localization Configuration
    var supportedCultures = new[] { "en", "en-US", "es", "es-PE" };
    var localizationOptions = new RequestLocalizationOptions()
        .SetDefaultCulture(supportedCultures[0])
        .AddSupportedCultures(supportedCultures)
        .AddSupportedUICultures(supportedCultures);
localizationOptions.ApplyCurrentCultureToResponseHeaders = true;
app.UseRequestLocalization(localizationOptions);


app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();

//"DefaultConnection": "server=warismart-db.mysql.database.azure.com;user=teamwarismart@warismart-db;password=Warismartpassword123;database=WarismartDB"