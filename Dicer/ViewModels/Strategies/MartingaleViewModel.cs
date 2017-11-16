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

        AutomatedBetSettings _settings;
        #endregion

        public MartingaleViewModel(DiceSite Site, AutomatedBetSettings settings)
            : base(Site)
        {
            StartCommand = new Command(async () => 
            {
                stop = false;
                if (_onLose == 0)
                    _onLose = (BetMultiplier / (BetMultiplier - 1)) * 1.05m;

                Strategy = new Martingale(_settings = settings);
                Strategy.Refresh += (obj, arg) =>
                {
                    RefreshValues();
                };
                await Strategy.Run(_site);  
            });

            StopCommand = new Command(() => Strategy.Stop());

            MessagingCenter.Subscribe<DiceSite, string>(this, "error", (sender, args) => Strategy.Stop());
            MessagingCenter.Subscribe<DiceSite, string>(this, "updateStatus", (sender, args) => Status = args);

            Title = "Martingale";
        }

        #region Properties
        protected IAutomationRunner Strategy { get; set; }

        public ICommand StartCommand { get; private set; }
        public ICommand StopCommand { get; private set; }

        public int Won => _site.wins;
        public int Loss => _site.losses;

        public decimal Luck
        {
            get
            {
                if (_settings == null || _settings.Chance == 0)
                    return 0m;

                return ((Won / (Won + Loss) * 10000) / _settings.Chance);
            }
        } 

        public decimal Wagered => _site.wagered;

        public decimal Profit => _site.profit;

        public decimal Balance => _site.balance;
		#endregion

		#region Methods
		

        #endregion
    }
}
