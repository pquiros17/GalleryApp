using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GalleryApp.ViewModel;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace GalleryApp.Model
{
    public class RecuerdoModel : NotifyPropertyChanged
    {
        #region Properties

        public int Id_Usuario { get; set; }

        public int Id_Recuerdo { get; set; }

        private string _descripcion_recuerdo { get; set; }
        public string DescripcionRecuerdo
        {
            get

            {
                return _descripcion_recuerdo;
            }
            set

            {
                _descripcion_recuerdo = value;
                OnPropertyChanged("DescripcionRecuerdo");
            }
        }

        public string FechaRecuerdo { get; set; }
       
        private byte[] _foto { get; set; }
        public byte[] Foto
        {
            get

            {
                return _foto;
            }
            set

            {
                _foto = value;
                OnPropertyChanged("Foto");
            }
        }
        private ImageSource fotoSource
        {
            get; set;

        }

        public ImageSource FotoSource
        {
            get

            {
                return fotoSource;
            }
            set

            {
                fotoSource = value;
                OnPropertyChanged("FotoSource");
            }
        }

        #endregion

        #region Methods
        public async static Task<ObservableCollection<RecuerdoModel>> ObtenerRecuerdos()
        {
            using (HttpClient client = new HttpClient())
            {

                var uri = new Uri("http://20710e5a.ngrok.io/api/values");

                HttpResponseMessage response = await client.GetAsync(uri).ConfigureAwait(false);

                string ans = await response.Content.ReadAsStringAsync();

                var lstPersonas = JsonConvert.DeserializeObject<ObservableCollection<RecuerdoModel>>(ans);

                return lstPersonas;
            }
        }

        public async static Task<bool> AñadirRecuerdo(RecuerdoModel recuerdo)
        {
            using (HttpClient client = new HttpClient())
            {
                var uri = new Uri("http://44da54f1.ngrok.io/api/Recuerdo/ObtenerRecuerdo");

                var json = JsonConvert.SerializeObject(new
                {
                    recuerdo.Id_Usuario,
                    recuerdo.DescripcionRecuerdo,
                    recuerdo.FechaRecuerdo,
                    recuerdo.Foto
                });

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(uri, content).ConfigureAwait(false);
                string ans = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<bool>(ans);
            }

        }

        #endregion

       
    }
}
