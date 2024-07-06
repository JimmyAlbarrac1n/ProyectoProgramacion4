using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using API_ProyectoFinal.Data.Models;

namespace API_ProyectoFinal.Data
{
    public class API_ProyectoFinalContext : DbContext
    {
        public API_ProyectoFinalContext (DbContextOptions<API_ProyectoFinalContext> options)
            : base(options)
        {
        }

        public DbSet<API_ProyectoFinal.Data.Models.Pelicula> Pelicula { get; set; } = default!;
        public DbSet<API_ProyectoFinal.Data.Models.Cartelera> Cartelera { get; set; } = default!;
    }
}
