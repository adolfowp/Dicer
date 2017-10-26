using System;
using System.Collections.Generic;
using System.Text;

namespace Dicer.Models
{
    [Flags]
    public enum BetResultAction : int
    {
        ReturnToBase = 1,
        Increase = 2,
        ChangeOdds = 4,
    }

    public enum BetOnEnum
    {
        High,
        Low,
        Alternate,
    }
}
