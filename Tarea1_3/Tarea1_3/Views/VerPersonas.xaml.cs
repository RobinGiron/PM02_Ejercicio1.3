using Syncfusion.ListView.XForms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tarea1_3.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tarea1_3.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VerPersonas : ContentPage
    {
        private Personas tmpPersona = new Personas();
        int itemIndex = -1;
        Image rightImage;
        Image leftImage;
        public VerPersonas()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            cargalista();
        }
        public static bool IsEmpty<Personas>(List<Personas> list)
        {
            if (list == null)
            {
                return true;
            }

            return !list.Any();
        }
        private async void cargalista()
        {
            var listPersonas = await App.BaseDatos.getListPersonas();
            bool isEmpty = IsEmpty(listPersonas);
            if (isEmpty)
            {
                if (listPersonas != null) { listaPersonas.ItemsSource = listPersonas; }
            }
            else
            {
                listPersonas.Clear();
                var listPersonas2 = await App.BaseDatos.getListPersonas();
                if (listPersonas2 != null) { listaPersonas.ItemsSource = listPersonas2; }
            }
        }

        private async void listPersonas_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (Personas)e.SelectedItem;
            btnactualiza.IsVisible = true;
            btnborra.IsVisible = true;
            var iduser = item.id;
            if (iduser != 0)
            {
                var personaa = await App.BaseDatos.getPersonById(iduser);
                if (personaa != null)
                {
                    tmpPersona.id = personaa.id;
                    tmpPersona.name = personaa.name;
                    tmpPersona.sname = personaa.sname;
                    tmpPersona.edad = personaa.edad;
                    tmpPersona.dir = personaa.dir;
                    tmpPersona.email = personaa.email;
                }
            }
        }

        private async void btnactualiza_Clicked(object sender, EventArgs e)
        {
            var ViewEditar = new EditPersonas();
            ViewEditar.BindingContext = tmpPersona;
            btnactualiza.IsVisible = false;
            btnborra.IsVisible = false;
            await Navigation.PushAsync(ViewEditar);
        }

        private async void btnborra_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("ALERTA", "¿DESEAR BORRAR ESTA PERSONA?", "Yes", "No");
            if (answer)
            {
                var personaa = await App.BaseDatos.getPersonById(tmpPersona.id);

                if (personaa != null)
                {
                    await App.BaseDatos.deletePerson(personaa);

                    await DisplayAlert("INFO", "SE ELIMINO LA PERSONA", "OK");
                    btnactualiza.IsVisible = false;
                    btnborra.IsVisible = false;
                    cargalista();
                }
            }
        }
        public void OnMore(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            DisplayAlert("More Context Action", mi.CommandParameter + " more context action", "OK");
        }

        public void OnDelete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            DisplayAlert("Delete Context Action", mi.CommandParameter + " delete context action", "OK");
        }
        private async void reff()
        {
            cargalista();
            await Task.Delay(2000);
        }
        private void ListView_SwipeStarted(object sender, Syncfusion.ListView.XForms.SwipeStartedEventArgs e)
        {
            itemIndex = -1;
        }

        private void ListView_Swiping(object sender, SwipingEventArgs e)
        {
            if (e.ItemIndex == 1 && e.SwipeOffSet > 70)
                e.Handled = true;
        }

        private void ListView_SwipeEnded(object sender, Syncfusion.ListView.XForms.SwipeEndedEventArgs e)
        {
            itemIndex = e.ItemIndex;
        }

        private void Image_BindingContextChanged(object sender, EventArgs e)
        {
            if (rightImage == null)
            {
                rightImage = sender as Image;
                (rightImage.Parent as View).GestureRecognizers.Add(new TapGestureRecognizer() { Command = new Command(Delete) });
            }
        }
        private async void Delete()
        {
            bool answer = await DisplayAlert("ALERTA", "¿DESEAR BORRAR ESTA PERSONA?", "Yes", "No");
            if (answer)
            {
                var personaa = await App.BaseDatos.getPersonById(tmpPersona.id);

                if (personaa != null)
                {
                    await App.BaseDatos.deletePerson(personaa);

                    await DisplayAlert("INFO", "SE ELIMINO LA PERSONA", "OK");
                    btnactualiza.IsVisible = false;
                    btnborra.IsVisible = false;
                    cargalista();
                }
            }
            this.listaPersonas.ResetSwipe();
        }

        private void Image_BindingContextChanged_1(object sender, EventArgs e)
        {
            if (leftImage == null)
            {
                leftImage = sender as Image;
                (leftImage.Parent as View).GestureRecognizers.Add(new TapGestureRecognizer() { Command = new Command(Update) });
            }
        }
        private async void Update()
        {
            var ViewEditar = new EditPersonas();
            ViewEditar.BindingContext = tmpPersona;
            btnactualiza.IsVisible = false;
            btnborra.IsVisible = false;
            await Navigation.PushAsync(ViewEditar);
            this.listaPersonas.ResetSwipe();
        }
        

        private async void listaPersonas_SelectionChanged(object sender, ItemSelectionChangedEventArgs e)
        {
            var items = e.AddedItems;
            var index = listaPersonas.DataSource.DisplayItems.IndexOf((Personas)items[0]);
            var valor = (Personas)listaPersonas.SelectedItem;
            
            btnactualiza.IsVisible = true;
            btnborra.IsVisible = true;
            if (valor.id !=0)
            {
                var personaa = await App.BaseDatos.getPersonById(valor.id);
                if (personaa != null)
                {
                    tmpPersona.id = personaa.id;
                    tmpPersona.name = personaa.name;
                    tmpPersona.sname = personaa.sname;
                    tmpPersona.edad = personaa.edad;
                    tmpPersona.dir = personaa.dir;
                    tmpPersona.email = personaa.email;
                }
            }
        }
    }
}