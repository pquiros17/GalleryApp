using GalleryApp.Model;
using Xamarin.Essentials;

namespace GalleryApp.ViewModel
{
    public class CarouViewModel : NotifyPropertyChanged
    {
        private InfoDispositivo _infoDispositivo = new InfoDispositivo();
        public InfoDispositivo infoDispositivo
        {
            get
            {
                return _infoDispositivo;
            }
            set
            {
                _infoDispositivo = value;
                OnPropertyChanged("infoDispositivo");
            }
        }
        public CarouViewModel()
        {
            InicializarInfoDispositivo();
        }
        public void InicializarInfoDispositivo()
        {
            infoDispositivo.Modelo = DeviceInfo.Model;
            infoDispositivo.Marca = DeviceInfo.Manufacturer;
            infoDispositivo.Nombre = DeviceInfo.Name;
            infoDispositivo.Version = DeviceInfo.Version.ToString();
            infoDispositivo.Plataforma = DeviceInfo.Platform.ToString();
            infoDispositivo.Idioma = DeviceInfo.Idiom.ToString();
            infoDispositivo.Tipo = DeviceInfo.DeviceType.ToString();
        }
    }
}
