using GaleryApp.View;
using GalleryApp.Model;
using GalleryApp.View;
using GTIApp.View;
using Realms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GalleryApp.ViewModel
{
    public class HomeViewModel : NotifyPropertyChanged
    {
        Realm realms;
        #region Properties
        private ObservableCollection<MenuModel> _lstMenu = new ObservableCollection<MenuModel>();
        public ObservableCollection<MenuModel> lstMenu
        {
            get
            {
                return _lstMenu;
            }
            set
            {
                _lstMenu = value;
                OnPropertyChanged("lstMenu");
            }
        }
        public int _idIngreso { get; set; }
        public int IdIngreso
        {
            get
            {
                return _idIngreso;
            }
            set
            {
                _idIngreso = value;
                OnPropertyChanged("IdIngreso");
            }
        }

        public static int _idusuario{ get; set; }
        public static string _nombre { get; set; }
        public static string _Fecha { get; set; }

        public string _nombreIngresa { get; set; }
        public string NombreIngresa
        {
            get
            {
                return _nombreIngresa;
            }
            set
            {
                _nombreIngresa = value;
                OnPropertyChanged("NombreIngresa");
            }
        }
        public string _fechaIngreso { get; set; }
        public string FechaIngreso
        {
            get
            {
                return _fechaIngreso;
            }
            set
            {
                _fechaIngreso = value;
                OnPropertyChanged("FechaIngreso");
            }
        }
        #endregion Properties


        public ICommand EnterMenuOptionCommand { get; set; }

        public HomeViewModel()
        {
            lstMenu = MenuModel.ObtenerMenu();
            IdIngreso = _idusuario;
            NombreIngresa = _nombre;
            FechaIngreso = _Fecha;
            InitializePersonCommands();
        }

        protected void InitializePersonCommands()
        {
            EnterMenuOptionCommand = new Command<int>(EnterMenuOption);
        }

        public void EnterMenuOption(int Id)
        {
            RecuerdoViewModel.DeleteInstance();
            ((MasterDetailPage)App.Current.MainPage).IsPresented = false;
            switch (Id)
            {
                case 1:
                    ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new MainRecuerdoView());
                    break;
                //case 2:
                //   ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new TabbedPageHomeView());
                //    break;
                case 2:
                    ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new CarouView());
                    break;
                case 4:
                    App.Current.MainPage = new LoginView();
                    break;
                default:
                    break;
            }
        }
      
    }
}