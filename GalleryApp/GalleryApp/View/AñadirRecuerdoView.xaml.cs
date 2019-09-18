using System;
using System.Collections.Generic;
using System.IO;
using GalleryApp.Model;
using GalleryApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GalleryApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AñadirRecuerdoView : ContentPage
    {
        public AñadirRecuerdoView()
        {
            InitializeComponent();

            BindingContext = RecuerdoViewModel.GetInstance();
            
        }

        private async void CameraButton_Clicked(object sender, EventArgs e)
        {
          
            //Espera a que se mtome la foto
            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions() { });
            if (photo != null)
            {//PhotoImage es el de la View
                PhotoImage.Source = ImageSource.FromStream(() => { return photo.GetStream(); });//Funcion anónima
                
                using (var memoryStream = new MemoryStream())
                {
                    photo.GetStream().CopyTo(memoryStream);
                    photo.Dispose();
                    RecuerdoViewModel.GetInstance().CurrentRecuerdo.Foto =  memoryStream.ToArray();
                }
            }
        }

        private byte[] GetImageStreamAsBytes(Stream input)
        {
            var buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}
