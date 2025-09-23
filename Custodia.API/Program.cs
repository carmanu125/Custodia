using Custodia.Application.Common.Interfaces;
using Custodia.Application.Services;
using Custodia.Infrastructure.Persistence;
using Custodia.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// PostgreSQL
builder.Services.AddDbContext<CustodiaDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("CustodiaDB")));

// Inyección de dependencias
builder.Services.AddScoped<IContratoRepository, ContratoRepository>();
builder.Services.AddScoped<ContratoService>();

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
