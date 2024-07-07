using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppProyectoFinal.ViewModels
{
    public class VerGuardadosViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<SavedCharacter> SavedCharacters { get; set; }

        public VerGuardadosViewModel()
        {
            LoadCharactersCommand = new Command(async () => await LoadCharacters());
            SavedCharacters = new ObservableCollection<SavedCharacter>();
            LoadCharactersCommand.Execute(null);
        }

        public Command LoadCharactersCommand { get; }

        private async Task LoadCharacters()
        {
            var characters = await App.Database.GetSavedCharactersAsync();
            SavedCharacters.Clear();
            foreach (var character in characters)
            {
                SavedCharacters.Add(character);
            }
        }
    }
}
