using System;

using Xamarin.Forms;
using Xamarin.Forms.DataGrid;

namespace Dicer
{
    public class BetRowTemplate : ViewCell
    {
        public BetRowTemplate()
        {
            var betInfo = BindingContext as Bet;
            var dg = new DataGrid();
            var cp = new DataGridColumn();
        }
    }
}
