using Dicer.Models;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Dicer
{
    public class MartingaleViewModel : PlayViewModel
    {
        #region Fields
        decimal _baseBalance;

        decimal _startingBet;
        decimal _onLose;
        decimal _onWin;

		bool high = true;
		bool starthigh = true;
		private bool withdrew;
		DateTime dtStarted = new DateTime();
		DateTime dtLastBet = new DateTime();
		TimeSpan TotalTime = new TimeSpan(0, 0, 0);

        private bool _returnToBaseOnLose = true;
        private bool _returnToBaseOnWin = true;
        private bool _isLimitOnProfitEnabled = false;
        private bool _isLimitOnLossesEnabled = false;
        private bool _isIncrementOnLoseEnabled = false;
        private bool _isIncrementOnWinEnabled = false;
        #endregion

        public MartingaleViewModel(DiceSite Site, AutomatedBetSettings settings)
            : base(Site)
        {
            StartCommand = new Command(async () => 
            {
                stop = false;
                if (_onLose == 0)
                    _onLose = (BetMultiplier / (BetMultiplier - 1)) * 1.05m;

                Strategy = new Martingale(settings);
                await Strategy.Run(_site);  
            });

            StopCommand = new Command(() => Strategy.Stop());

            Title = "Martingale";
        }

        #region Properties
        protected IAutomationRunner Strategy { get; set; }

        public ICommand StartCommand { get; private set; }
        public ICommand StopCommand { get; private set; }

        public int Won => _site.wins;
        public int Loss => _site.losses;
		#endregion

		#region Methods
		

        #endregion
    }
}
