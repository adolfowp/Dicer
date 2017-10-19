using System;
namespace Dicer
{
    public class DisconnectViewModel : BaseViewModel
    {
        #region Fields
        float donationPerc = 1.0f;
        #endregion
        public DisconnectViewModel()
        {
        }

        #region Properties
        public float DonationPercentage
        {
            get { return donationPerc; }
            set { SetProperty(ref donationPerc, value); }
        }
        #endregion
    }
}
