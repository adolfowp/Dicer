using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Dicer
{
    public partial class NewAccountPage : ContentPage
    {
        public NewAccountPage()
        {
            InitializeComponent();
        }

        async void HandleNewAccountSelected(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new LoginPage(e.Item as DiceSite));
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            Navigation.RemovePage(this);
        }
    }
}
