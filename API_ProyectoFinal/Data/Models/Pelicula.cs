using System;
using System.Collections.Generic;

namespace API_ProyectoFinal.Data.Models;

public partial class Pelicula
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Sinopsis { get; set; } = null!;

    public string? UrlImagen { get; set; }

    public int CarteleraId { get; set; }

    public int Duracion { get; set; }

    public float Precio { get; set; }

    public virtual Cartelera Cartelera { get; set; } = null!;
}
