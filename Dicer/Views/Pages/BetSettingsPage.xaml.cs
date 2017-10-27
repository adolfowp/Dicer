using System;
using System.Collections.Generic;
using Dicer.Models;
using Xamarin.Forms;

namespace Dicer
{
    public partial class BetSettingsPage : ContentPage
    {
        AutomatedBetSettings betSettings = new AutomatedBetSettings();
        DiceSite _site;

        protected BetSettingsPage()
        {
            InitializeComponent();
        }

        public BetSettingsPage(DiceSite Site)
            : this()
        {
            BindingContext = betSettings;

            _site = Site;
        }

        #region Methods
        private void SetCultureForControls()
        {
           // BaseBetEntry.s
        }
        #endregion

        private async void OnStartClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AutomaticPlayPage(_site, betSettings));
        }
    }
}
