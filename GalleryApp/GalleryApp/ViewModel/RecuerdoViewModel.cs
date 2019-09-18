using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalleryApp.Model;
using GalleryApp.View;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace GalleryApp.ViewModel
{
    public class RecuerdoViewModel : NotifyPropertyChanged
    {

        #region Properties

        private ObservableCollection<RecuerdoModel> _lstRecuerdo = new ObservableCollection<RecuerdoModel>();

        public ObservableCollection<RecuerdoModel> LstRecuerdo
        {
            get
            {
                return _lstRecuerdo;
            }
            set
            {
                _lstRecuerdo = value;
                OnPropertyChanged("LstRecuerdo");
            }
        }

        private RecuerdoModel _currentRecuerdo = new RecuerdoModel();

        public RecuerdoModel CurrentRecuerdo
        {
            get
            {
                return _currentRecuerdo;
            }
            set
            {
                _currentRecuerdo = value;
                OnPropertyChanged("CurrentRecuerdo");
            }
        }

        public ICommand AddNewPersonCommand { get; set; }
        public ICommand EnterAddNewPersonCommand { get; set; }

        public ICommand DeletePersonCommand { get; set; }

        public ICommand EnterEditPersonCommand { get; set; }

        #endregion

        #region Singleton
        private static RecuerdoViewModel instance = null;

        private RecuerdoViewModel()
        {
            InitCommands();
            ObtenerRecuerdosAsyn(CurrentRecuerdo);
        }

        public static RecuerdoViewModel GetInstance()
        {
            if (instance == null)
            {
                instance = new RecuerdoViewModel();
            }
            return instance;
        }

        public static void DeleteInstance()
        {
            if (instance != null)
            {
                instance = null;
            }
        }
        #endregion

        #region Methods

        private void AñadirNuevoRecuerdo()
        {

            if ( CurrentRecuerdo.Id_Recuerdo == 0)
            {
                CurrentRecuerdo.Id_Usuario = HomeViewModel._idusuario;
                CurrentRecuerdo.Id_Recuerdo = LstRecuerdo.Count() + 1;
                CurrentRecuerdo.FechaRecuerdo = DateTime.Now.ToShortDateString();
                AñadirNuevoRecuerdoAsyn(CurrentRecuerdo);
                
                //LstRecuerdo.Add(CurrentRecuerdo);
            }
            else
            {
                foreach (var item in LstRecuerdo)
                {
                    if (item.Id_Recuerdo == CurrentRecuerdo.Id_Recuerdo)
                    {
                        item.Id_Recuerdo = CurrentRecuerdo.Id_Recuerdo;
                        item.DescripcionRecuerdo = CurrentRecuerdo.DescripcionRecuerdo;
                        item.FechaRecuerdo = CurrentRecuerdo.FechaRecuerdo;
                        item.Foto = CurrentRecuerdo.Foto;
                        ActualizarRecuerdoAsyn(item);
                    }

                   
                }
            }
           
            

            CurrentRecuerdo = new RecuerdoModel();

            ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PopAsync();
        }
        public async static void AñadirNuevoRecuerdoAsyn(RecuerdoModel recuerdo)
        {
            
            using (HttpClient client = new HttpClient())
            {
                var uri = new Uri("http://b3c3c3e6.ngrok.io/api/Recuerdo/RegistrarRecuerdo");
                recuerdo.Id_Usuario = HomeViewModel._idusuario;
                recuerdo.FechaRecuerdo = DateTime.Now.ToShortDateString();
                var json = JsonConvert.SerializeObject(new
                {
                    recuerdo.Id_Usuario,
                    recuerdo.Id_Recuerdo,
                    recuerdo.DescripcionRecuerdo,
                    recuerdo.FechaRecuerdo,
                    recuerdo.Foto
                });

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(uri, content).ConfigureAwait(false);
                string ans = await response.Content.ReadAsStringAsync();

                 ObtenerRecuerdosAsyn(recuerdo);
               
                
            }

        }

        public async static void ObtenerRecuerdosAsyn(RecuerdoModel recuerdo)
        {

            using (HttpClient client = new HttpClient())
            {
                var uri = new Uri("http://b3c3c3e6.ngrok.io/api/Recuerdo/ObtenerRecuerdo");

                recuerdo.Id_Usuario = HomeViewModel._idusuario;
                var json = JsonConvert.SerializeObject(new
                {
                    recuerdo.Id_Usuario

                });

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(uri, content).ConfigureAwait(false);
                string ans = await response.Content.ReadAsStringAsync();



                RecuerdoViewModel.GetInstance().LstRecuerdo = JsonConvert.DeserializeObject<ObservableCollection<RecuerdoModel>>(ans);
                for (int i = 0; i < RecuerdoViewModel.GetInstance().LstRecuerdo.Count; i++)
                {
                    Stream stream = new MemoryStream(RecuerdoViewModel.GetInstance().LstRecuerdo[i].Foto);
                    RecuerdoViewModel.GetInstance().LstRecuerdo[i].FotoSource =   ImageSource.FromStream(() => { return stream; });
                }
            }

        }

        public async static void ActualizarRecuerdoAsyn(RecuerdoModel recuerdo)
        {

            using (HttpClient client = new HttpClient())
            {
                var uri = new Uri("http://b3c3c3e6.ngrok.io/api/Recuerdo/ActualizarRecuerdo");
               
                var json = JsonConvert.SerializeObject(new
                {
                    recuerdo.Id_Usuario,
                    recuerdo.Id_Recuerdo,
                    recuerdo.DescripcionRecuerdo,
                    recuerdo.FechaRecuerdo,
                    recuerdo.Foto
                });

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(uri, content).ConfigureAwait(false);
                string ans = await response.Content.ReadAsStringAsync();

                ObtenerRecuerdosAsyn(recuerdo);
                
               
                 
            }

        }

        public async static void EliminarRecuerdoAsyn(RecuerdoModel recuerdo)
        {

            using (HttpClient client = new HttpClient())
            {
                var uri = new Uri("http://b3c3c3e6.ngrok.io/api/Recuerdo/EliminarRecuerdo");

                var json = JsonConvert.SerializeObject(new
                {
                    recuerdo.Id_Usuario,
                    recuerdo.Id_Recuerdo,
                    recuerdo.DescripcionRecuerdo,
                    recuerdo.FechaRecuerdo,
                    recuerdo.Foto
                });

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(uri, content).ConfigureAwait(false);
                string ans = await response.Content.ReadAsStringAsync();

                ObtenerRecuerdosAsyn(recuerdo);

               

               
            }

        }
        private void EnterEditPerson(int Id)
        {
            CurrentRecuerdo = LstRecuerdo.FirstOrDefault(x => x.Id_Recuerdo == Id);
           
           

            ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new AñadirRecuerdoView());
        }

        private void DeletePerson(int Id)
        {
            CurrentRecuerdo = LstRecuerdo.FirstOrDefault(x => x.Id_Recuerdo == Id);
           EliminarRecuerdoAsyn(CurrentRecuerdo);
           
            
            CurrentRecuerdo = new RecuerdoModel();

            
        }

        public void EnterAddNewPerson()
        {
            ((MasterDetailPage)App.Current.MainPage).Detail.Navigation.PushAsync(new AñadirRecuerdoView());
        }
        #endregion

        public void InitCommands()
        {
            AddNewPersonCommand = new Command(AñadirNuevoRecuerdo);
            EnterAddNewPersonCommand = new Command(EnterAddNewPerson);
            EnterEditPersonCommand = new Command<int>(EnterEditPerson);
            DeletePersonCommand = new Command<int>(DeletePerson);
        }

      
    }
}
