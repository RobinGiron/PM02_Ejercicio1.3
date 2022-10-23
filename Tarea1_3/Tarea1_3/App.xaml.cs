using System;
using System.IO;
using Tarea1_3.Controller;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tarea1_3
{
    public partial class App : Application
    {
        static Database database;
        public static Database BaseDatos
        {
            get
            {
                if (database == null)
                {
                    database = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "db_personas.db3"));
                }

                return database;
            }


        }
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
