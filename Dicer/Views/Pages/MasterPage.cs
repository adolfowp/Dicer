using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Dicer.MasterDetail
{
    public class MasterPage : ContentPage
    {
        public ListView ListView { get { return listView; } }

        ListView listView;

        public MasterPage()
        {
            var masterPageItems = new List<MasterPageItem>();

            #region MasterPage Items Factory
            // Add items to the master page
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Manual Strategy",
                IconSource = "ic_format_paint",
                TargetType = typeof(ManualPlayPage),
            });
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Automatic Strategy",
                IconSource = "ic_play_circle_filled",
                TargetType = typeof(BetSettingsPage),
            });
            //masterPageItems.Add(new MasterPageItem
            //{
            //    Title = "Bet Settings",
            //    IconSource = "ic_settings",
            //    TargetType = typeof(BetSettingsPage),
            //});
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Earn Faucet",
                IconSource = "ic_monetization_on",
                TargetType = typeof(FaucetPage),
            });
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Disconnect",
                IconSource = "ic_power_settings_new",
                TargetType = typeof(DisconnectPage),
            });
            #endregion

            #region ListView data model
            listView = new ListView
            {
                ItemsSource = masterPageItems,
                ItemTemplate = new DataTemplate(() =>
                {
                    var grid = new Grid { Padding = new Thickness(5, 10) };
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(30) });
                    grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });

                    var image = new Image();
                    image.SetBinding(Image.SourceProperty, "IconSource");
                    var label = new Label { VerticalOptions = LayoutOptions.FillAndExpand };
                    label.SetBinding(Label.TextProperty, "Title");

                    grid.Children.Add(image);
                    grid.Children.Add(label, 1, 0);

                    return new ViewCell { View = grid };
                }),
                SeparatorVisibility = SeparatorVisibility.None,
            };
            #endregion

            Title = "Dicer Bot - by Lomba";
            Padding = new Thickness(0, 40, 0, 0);
            Content = new StackLayout
            {
                Children = { listView }
            };
        }
    }
}

