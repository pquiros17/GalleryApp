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


namespace GalleryApp.ViewModel
{
    
    public class LoginViewModel : NotifyPropertyChanged
    {
        Realm realms;


        #region Properties
        private string user;
        private string password;

        public string User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
                OnPropertyChanged("User");
            }
        }
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }

        public ICommand LoginCommand { get; set; }//Es la interfaz para hacer bindig eventos y se comunica con la vista

        #endregion

        #region Methods

        public async void Loging()
        {
            var lista = realms.All<UsuarioModel>().ToList();
            var usuario = lista.Where(usu =>  usu.Usuario == User).Select(usu => usu).ToList();
            if ((User == null || Password == null) || User.Equals(String.Empty) || Password.Equals(String.Empty))
            {
                await App.Current.MainPage.DisplayAlert("Advertencia!", "Espacio de usuario o contraseña en blanco", "OK");
            }
            else
            {
                if (usuario.Count == 0)
                {
                    bool respúesta = await App.Current.MainPage.DisplayAlert("Informacion", "Desea Registrarse en GALLERYAPP?", "SI", "NO");
                    if (respúesta)
                    {
                        App.Current.MainPage = new IngresoView();
                    }

                }
                else
                {


                    if (User.Equals(string.Empty) || Password.Equals(string.Empty))
                    {
                        await App.Current.MainPage.DisplayAlert("Advertencia!", "Credenciales incorrectas", "OK");
                    }
                    else
                    {
                        if (User.Equals(usuario[0].Usuario) && Password.Equals(usuario[0].Contrasena))
                        {

                            HomeViewModel._idusuario = usuario[0].Id;
                            HomeViewModel._nombre = usuario[0].Nombre;
                            HomeViewModel._Fecha = usuario[0].FechaIngreso;
                            //Pone al aplicativo en modo navegacion
                            NavigationPage navigation = new NavigationPage(new HomeView());
                            //NavigationPage navigation = new NavigationPage(new MainPersonView());
                            //Se direcciona
                            App.Current.MainPage = new MasterDetailPage
                            {
                                Master = new MenuView(),
                                Detail = navigation
                            };
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Advertencia!", "Credenciales incorrectas", "OK");

                        }
                    }





                }
            }
        }

        public LoginViewModel()
        {
            realms = Realm.GetInstance();
            LoginCommand = new Command(Loging);//Se bindea la funcion con el VIEW
        }

        #endregion

       
    }
}