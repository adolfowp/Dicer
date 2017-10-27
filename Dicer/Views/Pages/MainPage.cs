using System;
using Dicer.MasterDetail;
using Xamarin.Forms;

namespace Dicer
{
    public class MainPage : MasterDetailPage
    {
        MasterPage masterPage;
        DiceSite site;

        public MainPage(DiceSite connectedSite)
        {
            site = connectedSite;

            masterPage = new MasterPage();
            Master = masterPage;
            Detail = new NavigationPage(new ManualPlayPage(site));

            masterPage.ListView.ItemSelected += OnItemSelected;

            if(Device.RuntimePlatform == Device.UWP)
            {
                MasterBehavior = MasterBehavior.Popover;
            }
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterPageItem;
            if(item != null)
            {
                Detail = new NavigationPage((Page)Activator
                                            .CreateInstance(
                                                type: item.TargetType, 
                                                args: new object[] { site }));
                
                masterPage.ListView.SelectedItem = null;
                IsPresented = false;
            }
        }
    }
}
