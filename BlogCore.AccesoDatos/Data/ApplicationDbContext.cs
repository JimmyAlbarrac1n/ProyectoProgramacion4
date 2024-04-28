using BlogCore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace BlogCore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        //Aqui se colocan todos los modelos que se vayan creando

        public DbSet<Carteleras> Cartelera { get; set; }
        public DbSet<Pelicula> Pelicula { get; set; }



    }
}
