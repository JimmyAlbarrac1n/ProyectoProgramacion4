using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.Models
{
    public class Pelicula
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="El nombre es obligatorio")]
        [Display(Name ="Nombre de la película")]
        public string? Nombre { get; set; }
        [Required(ErrorMessage = "La descripción es obligatoria")]
        public string? Sinopsis { get; set; }
        [DataType(DataType.Upload)]
        [Display(Name ="Imagen")]
        public string? UrlImagen { get; set; }

        [Required(ErrorMessage = "La clasificación es obligatoria")]
        public int CarteleraId { get; set; }

        [ForeignKey("CarteleraId")]
        public Carteleras? Cartelera { get; set; }

        [Required(ErrorMessage = "La duración es obligatoria")]
        [Range(20,500)]
        public int Duracion { get; set; }
        [Required(ErrorMessage = "El precio es obligatorio")]
        public float Precio { get; set; }
    }
}
