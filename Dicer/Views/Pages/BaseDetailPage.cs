using System;

using Xamarin.Forms;

namespace Dicer
{
    public class BaseDetailPage : ContentPage
    {
        protected DiceSite site;

        protected BaseDetailPage(DiceSite Site)
        {
            site = Site;
        }
    }
}

