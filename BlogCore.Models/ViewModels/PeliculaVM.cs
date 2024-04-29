using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.Models.ViewModels
{
    public class PeliculaVM
    {
        public Pelicula Pelicula { get; set; }
        public IEnumerable<SelectListItem>? ListaCarteleras { get; set; }
    }
}
