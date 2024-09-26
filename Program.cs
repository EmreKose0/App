using App.Data;
using App.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var conn = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApiDbContext>(options =>
        options.UseNpgsql(conn));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection için gerekli servisleri ekliyoruz.
builder.Services.AddScoped<DriversController>();

var app = builder.Build();

// Swagger Middleware'ini ekleyelim
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.MapGet("/", async (DriversController driversController) =>
{
    return await driversController.Get();
});

app.Run();

// Minimal API içinde controller'ını buraya entegre ettik:
public class DriversController
{
    private readonly ApiDbContext _dbContext;
    private readonly ILogger<DriversController> _logger;

    public DriversController(ILogger<DriversController> logger, ApiDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    } 

    public async Task<IResult> Get()
    {
        var drivers = new Driver()
        {
            DriverNumber = 11,
            Name = "Lewis Hamilton" // İsim düzeltilmiş
        };

        _dbContext.Drivers.Add(drivers);
        await _dbContext.SaveChangesAsync();
        
        var allDrivers = await _dbContext.Drivers.ToListAsync();

        return Results.Ok(allDrivers);
    }
}
