using System;
using System.Collections.Generic;
using GalleryApp.ViewModel;
using Xamarin.Forms;

namespace GaleryApp.View
{
    public partial class MainRecuerdoView : ContentPage
    {
        public MainRecuerdoView()
        {
            InitializeComponent();
            
            BindingContext =  RecuerdoViewModel.GetInstance();
        }
    }
}
