using GalleryApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GalleryApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IngresoView : ContentPage
    {
        public IngresoView()
        {
            InitializeComponent();
            BindingContext = new IngresoViewModel();//Se le dice que el contexto sera la clase, donde el binding context sera el LoginViewModel
        }
    }
}