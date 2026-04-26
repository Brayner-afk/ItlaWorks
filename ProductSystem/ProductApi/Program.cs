using Microsoft.EntityFrameworkCore;
using ProductApi.Data;
using ProductApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// SQLite Configuration
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repository Registration
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// CORS Configuration - CRITICO para que Blazor funcione
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Forzamos el uso de CORS antes que otros middlewares
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

// Si la API corre, verás este mensaje en la consola
Console.WriteLine("API de Productos corriendo en: http://localhost:5117");

app.Run();
