using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GalleryApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PhotoView : ContentPage
    {
        public PhotoView()
        {
            InitializeComponent();
        }

        private async void CameraButton_Clicked(object sender, EventArgs e)
        {
            //Espera a que se mtome la foto
            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions() { });
            if (photo != null)//PhotoImage es el de la View
                PhotoImage.Source = ImageSource.FromStream(() => { return photo.GetStream(); });//Funcion anónima
        }
    }
}