using Microsoft.EntityFrameworkCore;
using API_ProyectoFinal.Data;
using API_ProyectoFinal.Data.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
namespace API_ProyectoFinal.Controllers;

public static class CarteleraEndpoints
{
    public static void MapCarteleraEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Cartelera").WithTags(nameof(Cartelera));

        group.MapGet("/", async (API_ProyectoFinalContext db) =>
        {
            return await db.Cartelera.ToListAsync();
        })
        .WithName("GetAllCarteleras")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Cartelera>, NotFound>> (int id, API_ProyectoFinalContext db) =>
        {
            return await db.Cartelera.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Cartelera model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetCarteleraById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Cartelera cartelera, API_ProyectoFinalContext db) =>
        {
            var affected = await db.Cartelera
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Id, cartelera.Id)
                    .SetProperty(m => m.Clasificacion, cartelera.Clasificacion)
                    .SetProperty(m => m.Genero, cartelera.Genero)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateCartelera")
        .WithOpenApi();

        group.MapPost("/", async (Cartelera cartelera, API_ProyectoFinalContext db) =>
        {
            db.Cartelera.Add(cartelera);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Cartelera/{cartelera.Id}",cartelera);
        })
        .WithName("CreateCartelera")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, API_ProyectoFinalContext db) =>
        {
            var affected = await db.Cartelera
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteCartelera")
        .WithOpenApi();
    }
}
