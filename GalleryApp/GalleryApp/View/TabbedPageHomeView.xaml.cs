using GalleryApp.ViewModel;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace GTIApp.View
{
    public partial class TabbedPageHomeView : TabbedPage
    {
        public TabbedPageHomeView()
        {
            InitializeComponent();
            BindingContext = new CarouViewModel();
        }
    }
}
