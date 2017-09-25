using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Dicer
{
    public class AccountsViewModel : BaseViewModel
    {
        public ObservableCollection<DiceSite> Accounts { get; set; }
        public Command LoadItemsCommand { get; set; }

        public AccountsViewModel()
        {
            Title = "Browse";
            Accounts = new ObservableCollection<DiceSite>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());


            MessagingCenter.Subscribe<LoginViewModel, DiceSite>(this, "AddAccount", (obj, item) =>
            {
                var _item = item as DiceSite;

                //Device.BeginInvokeOnMainThread(() => {  });
                Accounts.Add(_item);
                //await DataStore?.AddItemAsync(_item);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Accounts.Clear();
                var items = await DataStore?.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Accounts.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
