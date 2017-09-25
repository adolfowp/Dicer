using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Dicer
{
    public static class PlayPageFactory
    {
        private static Dictionary<Type, Type> typesMap =
            new Dictionary<Type, Type>()
            {
                { typeof(Dice999), typeof(PlayPage) },

            };

        public static ContentPage CreatePlayPage<T>(T SiteModel) where T : DiceSite
        {
            Type newPageType;

            if (!typesMap.TryGetValue(SiteModel.GetType(), out newPageType))
                return null;

            return (ContentPage)Activator.CreateInstance(
                newPageType, 
                args: new object[] { SiteModel });
        }
    }
}
