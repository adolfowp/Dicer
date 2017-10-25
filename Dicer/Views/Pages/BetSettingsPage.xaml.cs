using System;
using System.Collections.Generic;
using Dicer.Models;
using Xamarin.Forms;

namespace Dicer
{
    public partial class BetSettingsPage : ContentPage
    {
        AutomatedBetSettings betSettings = new AutomatedBetSettings();

        public BetSettingsPage()
        {
            InitializeComponent();
        }

        public BetSettingsPage(DiceSite Site)
            : this()
        {
            BindingContext = betSettings;
        }
    }
}
