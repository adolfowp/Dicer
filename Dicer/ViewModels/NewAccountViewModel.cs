using System;
using System.Collections.Generic;

namespace Dicer
{
    public class NewAccountViewModel : BaseViewModel
    {
        public NewAccountViewModel()
        {
            Title = "New Account";

            AccountsType = new List<DiceSite>()
            {
                new Bitsler(),
                new Dice999(true),
            };
        }

        public IEnumerable<DiceSite> AccountsType { get; set; }
    }
}
