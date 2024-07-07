using MauiAppProyectoFinal.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppProyectoFinal.Services
{
    public class PublicoService
    {
        public async Task<GuardarPelicula> GetImage(int id)
        {
            GuardarPelicula dto = null;
            HttpResponseMessage response;
            string requestUrl = $"https://rickandmortyapi.com/api/character/{id}";

            try
            {
                HttpClient client = new HttpClient();
                response = await client.GetAsync(requestUrl);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    dto = JsonConvert.DeserializeObject<GuardarPelicula>(json);
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                throw;
            }
            return dto;
        }
    }
}

