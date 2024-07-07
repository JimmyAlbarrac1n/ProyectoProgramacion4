using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppProyectoFinal.Models
{
    public class Pelicula
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string sinopsis { get; set; }
        public string urlImagen { get; set; }
        public int carteleraId { get; set; }
        public int duracion { get; set; }
        public int precio { get; set; }
        public object? cartelera { get; set; }
    }
}
