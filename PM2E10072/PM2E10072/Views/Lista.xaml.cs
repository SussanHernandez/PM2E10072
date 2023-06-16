using PM2E10072.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PM2E10072.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Lista : ContentPage
    {
        public Lista()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            list.ItemsSource = await App.Instancia.obtenerSitios();
        }

        private async void list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
             var objeto = (Sitios) list.SelectedItem;

             await App.Instancia.borrarSitio(objeto);

             await DisplayAlert("Aviso", "Sitio borrado con exito", "OK");

             await Navigation.PopAsync();
        }

    }
}