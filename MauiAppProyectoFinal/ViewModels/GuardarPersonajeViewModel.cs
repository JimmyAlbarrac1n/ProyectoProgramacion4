using MauiAppProyectoFinal.Models;
using MauiAppProyectoFinal.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppProyectoFinal.ViewModels
{
    public class GuardarPerosonajeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private readonly PublicoService service;

        public GuardarPerosonajeViewModel()
        {
            service = new PublicoService();
            SaveCharacterCommand = new Command(async () => await SaveCharacter());
            FetchCharacterCommand = new Command(async () => await FetchCharacter());
        }

        private int id;
        public int Id
        {
            get { return id; }
            set
            {
                if (value != id)
                {
                    id = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (value != name)
                {
                    name = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string status;
        public string Status
        {
            get { return status; }
            set
            {
                if (value != status)
                {
                    status = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private Uri imageUri;
        public Uri ImageUri
        {
            get { return imageUri; }
            set
            {
                if (value != imageUri)
                {
                    imageUri = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Command SaveCharacterCommand { get; }
        public Command FetchCharacterCommand { get; }

        private async Task SaveCharacter()
        {
            if (Id != 0)
            {
                var character = new GuardarPelicula
                {
                    Name = Name,
                    Status = Status,
                    Species = "Unknown", // Update with the actual species if available
                    ImageUrl = ImageUri.ToString()
                };

                await App.Database.SaveCharacterAsync(character);
                await Application.Current.MainPage.DisplayAlert("Éxito", "Personaje guardado correctamente", "OK");
            }
        }

        private async Task FetchCharacter()
        {
            var character = await service.GetImage(Id);
            if (character != null)
            {
                Name = character.name;
                Status = character.status;
                ImageUri = new Uri(character.imageUri);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Personaje no encontrado", "OK");
            }
        }
    }
}
