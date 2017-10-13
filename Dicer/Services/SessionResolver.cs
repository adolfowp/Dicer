using Dicer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dicer.Services
{
    static class SessionResolver
    {
        internal static ISession Login<Dice>(string username, string password, string twofa) where Dice : DiceSite
        {
            throw new NotImplementedException();
        }
    }
}
