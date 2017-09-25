using System;

using Xamarin.Forms;

namespace Dicer
{
    public class PlayPage : TabbedPage
    {
        public PlayPage(DiceSite Site)
        {
            #region Manual Bet Page
            var manualPage = new ManualPlayPage(Site);
            #endregion

            #region Automatic Bet Page
            var automaticPage = new AutomaticPlayPage(Site);
            #endregion

            Children.Add(manualPage);
            Children.Add(automaticPage);
        }
    }
}

