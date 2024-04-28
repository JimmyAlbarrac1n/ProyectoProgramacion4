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
    internal class CarteleraRepository : Repository<Carteleras>, ICarteleraRepository
    {
        private readonly ApplicationDbContext _db;
        public CarteleraRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Carteleras cartelera)
        {
            var objDesdeDb = _db.Cartelera.FirstOrDefault(s => s.Id == cartelera.Id);
           
            objDesdeDb.Clasificacion = cartelera.Clasificacion;
            objDesdeDb.Genero = cartelera.Genero;

            _db.SaveChanges();
        }
    }
}
