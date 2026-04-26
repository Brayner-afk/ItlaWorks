using Microsoft.EntityFrameworkCore;
using School.Application.Contract;
using School.Application.Service;
using School.Infrastructure.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// SQLite
builder.Services.AddDbContext<SchoolContext>(options =>
    options.UseSqlite("Data Source=school.db"));

// Registro de la Capa de Servicio
builder.Services.AddScoped<IDepartamentService, DepartamentService>();

var app = builder.Build();

app.UseAuthorization();
app.MapControllers();

app.Run();
