using Microsoft.EntityFrameworkCore;
using API_ProyectoFinal.Data;
using API_ProyectoFinal.Data.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
namespace API_ProyectoFinal.Controllers;

public static class PeliculaEndpoints
{
    public static void MapPeliculaEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Pelicula").WithTags(nameof(Pelicula));

        group.MapGet("/", async (API_ProyectoFinalContext db) =>
        {
            return await db.Pelicula.ToListAsync();
        })
        .WithName("GetAllPeliculas")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Pelicula>, NotFound>> (int id, API_ProyectoFinalContext db) =>
        {
            return await db.Pelicula.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Pelicula model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetPeliculaById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Pelicula pelicula, API_ProyectoFinalContext db) =>
        {
            var affected = await db.Pelicula
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Id, pelicula.Id)
                    .SetProperty(m => m.Nombre, pelicula.Nombre)
                    .SetProperty(m => m.Sinopsis, pelicula.Sinopsis)
                    .SetProperty(m => m.UrlImagen, pelicula.UrlImagen)
                    .SetProperty(m => m.CarteleraId, pelicula.CarteleraId)
                    .SetProperty(m => m.Duracion, pelicula.Duracion)
                    .SetProperty(m => m.Precio, pelicula.Precio)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdatePelicula")
        .WithOpenApi();

        group.MapPost("/", async (Pelicula pelicula, API_ProyectoFinalContext db) =>
        {
            db.Pelicula.Add(pelicula);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Pelicula/{pelicula.Id}",pelicula);
        })
        .WithName("CreatePelicula")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, API_ProyectoFinalContext db) =>
        {
            var affected = await db.Pelicula
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeletePelicula")
        .WithOpenApi();
    }
}
