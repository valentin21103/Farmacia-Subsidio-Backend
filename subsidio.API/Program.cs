using Microsoft.EntityFrameworkCore;
using subsidio.Business.services;
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

// --- INICIO DEL CAMBIO (Habilitar CORS - Parte 1) ---
builder.Services.AddCors(options =>
{
    options.AddPolicy("NuevaPolitica", app =>
    {
        app.AllowAnyOrigin()   // Permite que Angular (localhost:4200) entre
           .AllowAnyHeader()
           .AllowAnyMethod();
    });
});
// --- FIN DEL CAMBIO ---

builder.Services.AddControllers();

// Configuración para que funcione Swagger (Swashbuckle)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ZONA DE INYECCIÓN DE DEPENDENCIAS
builder.Services.AddScoped<SolicitudService>();
// Asegúrate de que IUsuarioService e IMedicamentoService existan
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IMedicamentoService, MedicamentoService>();

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

// --- INICIO DEL CAMBIO (Usar CORS - Parte 2) ---
// ¡IMPORTANTE! Esta línea debe ir ANTES de UseAuthorization
app.UseCors("NuevaPolitica");
// --- FIN DEL CAMBIO ---

app.UseAuthorization();
app.MapControllers();

app.Run();