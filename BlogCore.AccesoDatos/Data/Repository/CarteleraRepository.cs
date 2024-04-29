using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Data;
using BlogCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public IEnumerable<SelectListItem> GetListaCarteleras()
        {
            return _db.Cartelera.Select(i => new SelectListItem()
            {
                Text = i.Clasificacion,
                Value = i.Id.ToString()
            });
        }

        

        public void Update(Carteleras cartelera)
        {
            var objDesdeDb = _db.Cartelera.FirstOrDefault(s => s.Id == cartelera.Id);
           
            objDesdeDb.Clasificacion = cartelera.Clasificacion;
            objDesdeDb.Genero = cartelera.Genero;

            //_db.SaveChanges();
        }
    }
}
