using System;
using System.Globalization;
using Xamarin.Forms;

namespace Dicer
{
    public partial class App : Application
    {
        public static bool UseLocalStore = true;
        public static string BackendUrl = "https://localhost:5000";

        public App()
        {
            InitializeComponent();

            if (UseLocalStore)
                DependencyService.Register<DiceFileDataStore>();
            else
                DependencyService.Register<CloudDataStore>();

            CultureInfo.CurrentUICulture = CultureInfo.InvariantCulture;

#if DEBUG
            var _999dice = new Dice999(false);
            _999dice.Login("cacca95", "caccacacca95", "");
            MainPage = new NavigationPage(new BetSettingsPage(_999dice));
#else
            MainPage = new NavigationPage(new AccountsPage());
#endif
        }
    }
}
