﻿using BlogCore.AccesoDatos.Data.Repository.IRepository;
using BlogCore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.AccesoDatos.Data.Repository
{
    public class ContenedorTrabajo : IContenedorTrabajo
    {
        private readonly ApplicationDbContext _db;
        public ContenedorTrabajo(ApplicationDbContext db)
        {
            _db = db;
            Cartelera = new CarteleraRepository(_db);
            Pelicula = new PeliculaRepository(_db);
            Slider = new SliderRepository(_db);
        }

        public ICarteleraRepository Cartelera { get; private set; }
        public IPeliculaRepository Pelicula { get; private set; }

        public ISliderRepository Slider { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
