using System.Collections.ObjectModel;

using Xamarin.Forms;

namespace GalleryApp.Model
{
    public class MenuModel : ContentView
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public string Icon { get; set; }
       

        public static ObservableCollection<MenuModel> ObtenerMenu()
        {
            ObservableCollection<MenuModel> vloMenus = new ObservableCollection<MenuModel>()
            {
                new MenuModel {Id=1, Name= "Mis Recuerdos", Detail="Visualiza los Recuerdos",Icon="img/Recuerdo.png"},
                //new MenuModel {Id=2, Name= "Crear Recuerdo", Detail="Almacena el Recuerdo.",Icon="img/Recuerdo.png"},
                new MenuModel {Id=2, Name= "Informacion de GalleryApp", Detail="Informacion del sistema.",Icon="img/InfoSis.png"},
                new MenuModel {Id=4, Name= "Salir", Detail="Salida del GalleryApp.",Icon="img/salir.png"}
            };
            return vloMenus;
        }
    }
}