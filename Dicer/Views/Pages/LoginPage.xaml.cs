using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Dicer
{
    public partial class LoginPage : ContentPage
    {
        readonly LoginViewModel viewmodel;

        public LoginPage()
        {
            InitializeComponent();
        }

        public LoginPage(DiceSite Site)
            : this()
        {
            BindingContext = viewmodel = new LoginViewModel(Site);
        }

        #region Methods
        protected override void OnAppearing()
        {
            base.OnAppearing();


        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            Navigation.RemovePage(this);
        }
        #endregion
    }
}
