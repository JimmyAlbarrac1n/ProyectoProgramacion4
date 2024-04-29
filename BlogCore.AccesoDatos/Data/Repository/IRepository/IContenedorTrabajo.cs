﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.AccesoDatos.Data.Repository.IRepository
{
    public interface IContenedorTrabajo : IDisposable
    {
        //Aqui se deben ir agregando los diferentes repositorios
        ICarteleraRepository Cartelera { get; }
        IPeliculaRepository Pelicula { get; }

        void Save();

    }
}
