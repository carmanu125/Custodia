using Custodia.Application.Common.Interfaces;
using Custodia.Application.Services;
using Custodia.Infrastructure.Persistence;
using Custodia.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// PostgreSQL
builder.Services.AddDbContext<CustodiaDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("CustodiaDB")));

// Habilitar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") // 👈 tu frontend
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Inyección de dependencias
builder.Services.AddScoped<IContratoRepository, ContratoRepository>();
builder.Services.AddScoped<ContratoService>();
builder.Services.AddScoped<IVigenciaRepository, VigenciaRepository>();
builder.Services.AddScoped<VigenciaService>();
builder.Services.AddScoped<IAnaquelRepository, AnaquelRepository>();
builder.Services.AddScoped<AnaquelService>();
builder.Services.AddScoped<ICajaRepository, CajaRepository>();
builder.Services.AddScoped<CajaService>();
builder.Services.AddScoped<IDependenciaRepository, DependenciaRepository>();
builder.Services.AddScoped<DependenciaService>();
builder.Services.AddScoped<IFolioRepository, FolioRepository>();
builder.Services.AddScoped<FolioService>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<UsuarioService>();

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Usar la política de CORS
app.UseCors("AllowAngular");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
