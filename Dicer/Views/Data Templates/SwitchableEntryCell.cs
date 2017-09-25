using System;

using Xamarin.Forms;

namespace Dicer.Views.DataTemplates
{
    public class SwitchableEntryCell : ViewCell
    {
        public SwitchableEntryCell()
        {
            #region Switch
            var enabler = new Switch();
            #endregion

            var mainLayout = new Grid();
            View = new Label { Text = "Hello ContentView" };
        }
    }
}

