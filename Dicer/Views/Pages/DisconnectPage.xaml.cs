using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Dicer
{
    public partial class DisconnectPage : ContentPage
    {
        DiceSite _site;
        public DisconnectPage(DiceSite site)
        {
            InitializeComponent();
            BindingContext = new DisconnectViewModel();
            _site = site;
        }

        public DisconnectPage()
            : this(null)
        {

        }
    }
}
