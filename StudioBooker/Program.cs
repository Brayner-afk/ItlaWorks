using Microsoft.EntityFrameworkCore;
using StudioBooker.Data;

var builder = WebApplication.CreateBuilder(args);

// Configurar SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Sembrar datos iniciales
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    
    if (!context.Clientes.Any())
    {
        context.Clientes.AddRange(
            new StudioBooker.Models.Cliente { Nombre = "Brayner Melo", Email = "brayner@example.com", Telefono = "809-555-0101" },
            new StudioBooker.Models.Cliente { Nombre = "Estudio Pro", Email = "pro@example.com", Telefono = "809-555-0102" }
        );
    }

    if (!context.Cabinas.Any())
    {
        // ... (existing cabina seeding code)
    }

    if (!context.Equipos.Any())
    {
        context.Equipos.AddRange(
            new StudioBooker.Models.Equipo { 
                Nombre = "Micrófono de Condensador", 
                Marca = "Shure", 
                Modelo = "SM7B", 
                Categoria = "Audio", 
                Descripcion = "El estándar de oro para podcasting y voces de radio.",
                Estado = "Disponible"
            },
            new StudioBooker.Models.Equipo { 
                Nombre = "Cámara Mirrorless 4K", 
                Marca = "Sony", 
                Modelo = "A7 IV", 
                Categoria = "Video", 
                Descripcion = "Captura video nítido con autoenfoque de ojos en tiempo real.",
                Estado = "Disponible"
            },
            new StudioBooker.Models.Equipo { 
                Nombre = "Aro de Luz Pro", 
                Marca = "Godox", 
                Modelo = "LR150", 
                Categoria = "Iluminación", 
                Descripcion = "Iluminación uniforme y suave para rostros.",
                Estado = "Disponible"
            },
            new StudioBooker.Models.Equipo { 
                Nombre = "Sillón Ergonómico Podcast", 
                Marca = "Secretlab", 
                Modelo = "Titan EVO", 
                Categoria = "Mobiliario", 
                Descripcion = "Comodidad suprema para sesiones de grabación largas.",
                Estado = "Disponible"
            }
        );
    }
    context.SaveChanges();
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
