using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Dicer
{
    public partial class MartingalePage : ContentPage
    {
        MartingaleViewModel viewmodel;

        public MartingalePage()
        {
            InitializeComponent();
        }

        public MartingalePage(DiceSite Site)
            : this()
        {
            BindingContext = viewmodel = new MartingaleViewModel(Site);
        }
    }
}
