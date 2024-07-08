using MauiAppProyectoFinal.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MauiAppProyectoFinal.Services
{
    public class PeliculaRepository
    {
        private readonly SQLiteAsyncConnection _database;

        public PeliculaRepository(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<GuardarPelicula>().Wait();
        }

        public Task<List<GuardarPelicula>> GetSavedCharactersAsync()
        {
            return _database.Table<GuardarPelicula>().ToListAsync();
        }

        public Task<int> SaveCharacterAsync(GuardarPelicula character)
        {
            return _database.InsertAsync(character);
        }
    }
}
