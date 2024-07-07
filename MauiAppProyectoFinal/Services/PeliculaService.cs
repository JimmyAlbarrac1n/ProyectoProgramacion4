using MauiAppProyectoFinal.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MauiAppProyectoFinal.Services
{
    public class PeliculaService
    {
        private readonly HttpClient _httpClient;

        public PeliculaService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Pelicula>> GetPeliculasAsync()
        {
            try
            {
                string url = "http://localhost:5230/api/Pelicula"; 
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Pelicula>>(json);
            }
            catch (Exception ex)
            {
                // Manejar excepción
                Console.WriteLine($"Error: {ex.Message}");
                return new List<Pelicula>();
            }
        }
    }


}
