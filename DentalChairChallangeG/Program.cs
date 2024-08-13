using DentalChairChallangeG.Services;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    if (string.IsNullOrEmpty(connectionString))
    {
        throw new InvalidOperationException("Connection string is not configured.");
    }

    options.UseMySql(
        connectionString,
        new MySqlServerVersion(new Version(8, 0, 21))
    );
});

builder.Services.AddScoped<IChairRepository, ChairRepository>();
builder.Services.AddScoped<ChairService>();

var app = builder.Build();

// Automatically apply migrations on startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await TestDatabaseConnection(app.Services);

app.Run();

async Task TestDatabaseConnection(IServiceProvider serviceProvider)
{
    using var scope = serviceProvider.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    try
    {
        // Try to open a connection to the database
        await dbContext.Database.OpenConnectionAsync();
        Console.WriteLine("Database connection successful.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Database connection failed: {ex.Message}");
    }
    finally
    {
        // Ensure the connection is closed
        await dbContext.Database.CloseConnectionAsync();
    }
}