using MauiAppProyectoFinal.Services;

namespace MauiAppProyectoFinal
{
    public partial class App : Application
    {
        static PeliculaRepository database;

        public static PeliculaRepository Database
        {
            get
            {
                if (database == null)
                {
                    database = new PeliculaRepository(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "characters.db3"));
                }
                return database;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
