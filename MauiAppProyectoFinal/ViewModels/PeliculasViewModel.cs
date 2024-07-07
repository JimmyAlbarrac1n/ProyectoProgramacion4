using MauiAppProyectoFinal.Services;
using MauiAppProyectoFinal.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace MauiAppProyectoFinal.ViewModels
{
    public class PeliculasViewModel : INotifyPropertyChanged
    {
        private readonly PeliculaService _peliculaService;

        private ObservableCollection<Pelicula> _peliculas;
        public ObservableCollection<Pelicula> Peliculas
        {
            get => _peliculas;
            set
            {
                _peliculas = value;
                OnPropertyChanged();
            }
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public PeliculasViewModel()
        {
            _peliculaService = new PeliculaService();
            Peliculas = new ObservableCollection<Pelicula>();
            LoadPeliculas();
        }

        private async void LoadPeliculas()
        {
            IsLoading = true;
            try
            {
                var peliculas = await _peliculaService.GetPeliculasAsync();
                Peliculas.Clear();
                foreach (var pelicula in peliculas)
                {
                    Peliculas.Add(pelicula);
                }
            }
            catch (Exception ex)
            {
                // Manejar excepción
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
