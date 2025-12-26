using Microsoft.EntityFrameworkCore;
using subsidio.Infraestructura.Data;

var builder = WebApplication.CreateBuilder(args);

// ---------------------------------------------------------
// 1. ZONA DE BASE DE DATOS
// ---------------------------------------------------------
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// ---------------------------------------------------------
// 2. SERVICIOS DE LA API Y SWAGGER
// ---------------------------------------------------------
builder.Services.AddControllers();

// Configuración para que funcione Swagger (Swashbuckle)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ---------------------------------------------------------
// 3. CONFIGURACIÓN VISUAL (PIPELINE)
// ---------------------------------------------------------
if (app.Environment.IsDevelopment())
{
    // Activar la interfaz gráfica de Swagger
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run(); 