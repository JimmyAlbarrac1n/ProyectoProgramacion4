using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.Models
{
    public class Sala
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="La capacidad es obligatoria")]
        public int capacidad { get; set; }

    }
}
