using System;
using System.Collections.Generic;

namespace API_ProyectoFinal.Data.Models;

public partial class Cartelera
{
    public int Id { get; set; }

    public string Clasificacion { get; set; } = null!;

    public string Genero { get; set; } = null!;

    public virtual ICollection<Pelicula> Peliculas { get; set; } = new List<Pelicula>();
}
