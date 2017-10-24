using System;
using System.Collections.Generic;
using System.Text;

namespace Dicer.Models
{
    [Flags]
    enum BetResultAction : int
    {
        ReturnToBase = 1,
        Increase = 2,
        ChangeOdds = 4,
    }

    enum BetOnEnum
    {
        High,
        Low,
        Alternate,
    }
}
