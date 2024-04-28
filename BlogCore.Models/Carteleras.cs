using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.Models
{
    public class Carteleras
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Ingrese la clasificacion de la pelicula")]
        [Display(Name ="Clasificacion de la película")]
        public string Clasificacion { get; set; }
        [Required(ErrorMessage = "Ingrese el genero de la pelicula")]
        [Display(Name = "Género de la película")]
        public string Genero { get; set; }

    }
}
