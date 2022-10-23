using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Tarea1_3.Views;
using SQLite;
using System.IO;

namespace Tarea1_3
{
    public partial class MainPage : ContentPage
    {
        string db_path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "db_personas.db3");
        public MainPage()
        {
            InitializeComponent();
        }

        private async void btnsalva_Clicked(object sender, EventArgs e)
        {

            var db_person = new SQLiteConnection(db_path);

            var person = new Models.Personas
            {
                name = txtname.Text,
                sname = txtsname.Text,
                edad = Convert.ToInt32(txtedad.Text),
                dir = txtdir.Text,
                email = txtemail.Text
            };
            var r = await App.BaseDatos.savePersona(person);
            if (r == 1)
            {
                await DisplayAlert("INFO", "Persona ingresada correctamente", "OK");
                var ListPersonas = await App.BaseDatos.getListPersonas();
            }
            else
            {
                await DisplayAlert("ALERTA", "No se pudo guardar la Persona", "OK");
            }
            limpiar();
        }

        private async void btnLista_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new VerPersonas());
        }
        private void limpiar()
        {
            this.txtname.Text = String.Empty;
            this.txtsname.Text = String.Empty;
            this.txtedad.Text = String.Empty;
            this.txtdir.Text = String.Empty;
            this.txtemail.Text = String.Empty;

        }
    }
}
