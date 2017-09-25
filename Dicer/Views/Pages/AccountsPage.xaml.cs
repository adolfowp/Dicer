using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Dicer
{
    public partial class AccountsPage : ContentPage
    {
        AccountsViewModel viewModel;

        public AccountsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new AccountsViewModel();

			MessagingCenter.Subscribe<LoginViewModel, DiceSite>(this, "AddAccount", (obj, item) =>
			{
				var _item = item as DiceSite;
                Device.BeginInvokeOnMainThread(async () => 
                               await Navigation.PushAsync(new PlayPage(item)));
			});
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Item;
            if (item == null)
                return;

            //await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            // Manually deselect item
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewAccountPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Accounts.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}
