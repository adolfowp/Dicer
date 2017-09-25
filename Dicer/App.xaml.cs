using System;

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


                MainPage = new NavigationPage(new MainPage());
        }
    }
}
