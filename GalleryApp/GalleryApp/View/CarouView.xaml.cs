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
    public partial  class CarouView : CarouselPage
    {
        public  CarouView()
        {
            InitializeComponent();
            BindingContext = new CarouViewModel();
           
        }
    }
}