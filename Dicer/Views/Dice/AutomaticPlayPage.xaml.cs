using Dicer.Models;
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

        protected AutomaticPlayPage()
        {
            InitializeComponent();
        }

        public AutomaticPlayPage(DiceSite Site, AutomatedBetSettings settings)
            : this()
        {
            BindingContext = viewmodel = new MartingaleViewModel(Site, settings);

            viewmodel.Title = _titleText;
        }

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            viewmodel.RefreshValues();
        }
    }
}
