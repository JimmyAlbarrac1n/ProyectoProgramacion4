using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using API_ProyectoFinal.Data;
using API_ProyectoFinal.Controllers;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<API_ProyectoFinalContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSQL") ?? throw new InvalidOperationException("Connection string 'API_ProyectoFinalContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
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

app.MapPeliculaEndpoints();

app.MapCarteleraEndpoints();

app.Run();
