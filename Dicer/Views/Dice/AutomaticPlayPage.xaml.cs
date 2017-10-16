using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Dicer
{
    public partial class AutomaticPlayPage : ContentPage
    {
        #region Fields
        readonly PlayViewModel viewmodel;
        const string _titleText = "Automatic";
        #endregion

        public AutomaticPlayPage()
        {
            InitializeComponent();
        }

        public AutomaticPlayPage(DiceSite Site)
            : this()
        {
            BindingContext = viewmodel = new MartingaleViewModel(Site);

            viewmodel.Title = _titleText;
        }
    }
}
