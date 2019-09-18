using GalleryApp.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Realms;
using GalleryApp.Model;
using GaleryApp.View;

namespace GalleryApp.ViewModel
{
    
    public class IngresoViewModel : NotifyPropertyChanged
    {
        Realm realms;


        #region Properties
        private int _id;
        private string _nombre;
        private string _usuario;
        private string _contrasena;
      
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                OnPropertyChanged("Password");
            }
        }
        public string Nombre
        {
            get
            {
                return _nombre;
            }
            set
            {
                _nombre = value;
                OnPropertyChanged("User");
            }
        }
        public string Usuario
        {
            get
            {
                return _usuario;
            }
            set
            {
                _usuario = value;
                OnPropertyChanged("User");
            }
        }
        public string Contrasena
        {
            get
            {
                return _contrasena;
            }
            set
            {
                _contrasena = value;
                OnPropertyChanged("Password");
            }
        }

       

        public ICommand IngresoCommand { get; set; }//Es la interfaz para hacer bindig eventos y se comunica con la vista

        #endregion

        #region Methods

        public async void Ingreso()
        {
           
            var lista = realms.All<UsuarioModel>().ToList();

            UsuarioModel usuario = new UsuarioModel();
            usuario.Id = lista.Count() + 1;
            usuario.Nombre = Nombre;
            usuario.Usuario = Usuario;
            usuario.Contrasena = Contrasena;
            usuario.FechaIngreso = DateTime.Now.ToShortDateString();

            using (var tran = realms.BeginWrite())
            {
                realms.Add(usuario);
                tran.Commit();
            }
            App.Current.MainPage = new LoginView();


        }

        public IngresoViewModel()
        {
            realms = Realm.GetInstance();
            IngresoCommand = new Command(Ingreso);//Se bindea la funcion con el VIEW
        }

        #endregion

       
    }
}