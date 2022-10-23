using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tarea1_3.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tarea1_3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditPersonas : ContentPage
    {
        public EditPersonas()
        {
            InitializeComponent();
        }

        private async void btnatras_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void btnedit_Clicked(object sender, EventArgs e)
        {
            var persons = new Personas()
            {
                id = Convert.ToInt32(txtid2.Text),
                name = txtname2.Text,
                sname = txtsname2.Text,
                edad = Convert.ToInt32(txtedad2.Text),
                dir = txtdir2.Text,
                email = txtemail2.Text
            };

            var r = await App.BaseDatos.savePersona(persons);
            if (r == 1)
            {
                await DisplayAlert("Actualizar", "Persona Actualizada correctamente", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Actualizar", "ERROR! No se pudo actualizar la Persona", "OK");
            }
        }
    }
}