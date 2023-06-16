using Plugin.Geolocator;
using Plugin.Media;
using PM2E10072.Models;
using PM2E10072.Views;
using System;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PM2E10072
{
    public partial class MainPage : ContentPage
    {
        Plugin.Media.Abstractions.MediaFile photo = null;
        public MainPage()
        {
            InitializeComponent();
            Localizar();
        }
        public String getImage64()
        {
            if(photo != null)
            {
                using (MemoryStream memory = new MemoryStream())
                {
                    Stream stream = photo.GetStream();
                    stream.CopyTo(memory);
                    byte[] fotobyte = memory.ToArray();

                    String Base64 = Convert.ToBase64String(fotobyte);
                    return Base64;
                }
            }

            return null;
        }

        public byte[] GetimageBytes()
        {
            if (photo != null)
            {
                using (MemoryStream memory = new MemoryStream())
                {
                    Stream stream = photo.GetStream();
                    stream.CopyTo(memory);
                    byte[] fotobyte = memory.ToArray();

                    return fotobyte;
                }

            }

            return null;
        }

        private async void btnGuardar_Clicked(object sender, EventArgs e)
        {
            if(imagen == null || elatitud.Text == null || elongitud.Text == null || description.Text == null)
            {
                await DisplayAlert("Aviso", "Agrege una imagen y escriba una descripcion", "OK");
            }
            else
            {
                var sitio = new Sitios
                {
                    latitud = Convert.ToDouble(elatitud.Text),
                    longitud = Convert.ToDouble(elongitud.Text),
                    descripcion = description.Text,
                    Imagen = GetimageBytes()
                };

                if (await App.Instancia.crearSitio(sitio) > 0)
                {
                    await DisplayAlert("Aviso", "Sitio agregado con exito", "OK");
                }
                else
                {
                    await DisplayAlert("Aviso", "Ha ocurrido un error", "OK");
                }
            }
        }

        private async void agregarImagen_Clicked(object sender, EventArgs e)
        {
            photo = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "MYAPP",
                Name = "Foto.jpg",
                SaveToAlbum = true
            });

            if (photo != null)
            {
                imagen.Source = ImageSource.FromStream(() => { return photo.GetStream(); });
            }

        }

  
        private async void Localizar()
        {
            var locator = CrossGeolocator.Current; //Acceso a la API
            locator.DesiredAccuracy = 50; //Precisión (en metros)
            if (locator.IsGeolocationAvailable) //Servicio existente en el dispositivo
            {
                if (locator.IsGeolocationEnabled) //GPS activado en el dispositivo
                {
                    if (!locator.IsListening) //Comprueba si el dispositivo escucha al servicio
                    {
                        await locator.StartListeningAsync(TimeSpan.FromSeconds(1), 5); //Inicio de la escucha
                    }
                    locator.PositionChanged += (cambio, args) =>
                    {
                        var loc = args.Position;
                        elongitud.Text = loc.Longitude.ToString();
                        /*longi = double.Parse(lon.Text);*/
                        elatitud.Text = loc.Latitude.ToString();
                        /*lati = double.Parse(lat.Text);*/
                    };
                }
            }
        }


        private async void btnLista_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.Lista());

        }

        private void btnPosicion_Clicked(object sender, EventArgs e)
        {
          
        }
    }
}
