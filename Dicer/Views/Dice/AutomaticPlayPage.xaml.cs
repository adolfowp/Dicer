using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Dicer
{
    public partial class AutomaticPlayPage : CarouselPage
    {
        #region Fields
        readonly PlayViewModel viewmodel;
        const string _titleText = "Automatic";
        #endregion

        public AutomaticPlayPage()
        {
            InitializeComponent();
        }

        public AutomaticPlayPage(DiceSite Site)
            : this()
        {
            BindingContext = viewmodel = new PlayViewModel(Site);

            viewmodel.Title = _titleText;

            Children.Add(new MartingalePage(Site));
        }
}
}
