using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Data;
using BlogCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.AccesoDatos.Data.Repository
{
    internal class PeliculaRepository : Repository<Pelicula>, IPeliculaRepository
    {
        private readonly ApplicationDbContext _db;
        public PeliculaRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Pelicula pelicula)
        {
            var objDesdeDb = _db.Pelicula.FirstOrDefault(s => s.Id == pelicula.Id);
           
            objDesdeDb.Nombre = pelicula.Nombre ;
            objDesdeDb.Sinopsis = pelicula.Sinopsis;
            objDesdeDb.UrlImagen = pelicula.UrlImagen;
            objDesdeDb.CarteleraId = pelicula.CarteleraId;
            objDesdeDb.Duracion = pelicula.Duracion;
            objDesdeDb.Precio = pelicula.Precio;


            //_db.SaveChanges();
        }


    }
}
