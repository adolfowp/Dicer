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
        public decimal BaseBalance
        {
            get { return _baseBalance; }
            set { SetProperty(ref _baseBalance, value); }
        }

        protected IAutomationRunner Strategy { get; set; }

        public ICommand StartCommand { get; private set; }
        public ICommand StopCommand { get; private set; }

        public decimal StartingBet
        {
            get { return _startingBet; }
            set { SetProperty(ref _startingBet, value); }
        }
        public decimal OnLose
        {
            get { return _onLose; }
            set { SetProperty(ref _onLose, value); }
        }
        public decimal OnWin
        {
            get { return _onWin; }
            set { SetProperty(ref _onWin, value); }
        }

        public bool ReturnToBaseOnLose
        {
            get { return _returnToBaseOnLose; }
            set
            {
                SetProperty(ref _returnToBaseOnLose, value,
                             onChanged: () => IncrementOnLoseEnabled = !value);
            }
        }

        public bool ReturnToBaseOnWin
        {
            get { return _returnToBaseOnWin; }
            set
            {
                SetProperty(ref _returnToBaseOnWin, value,
                             onChanged: () => IncrementOnWinEnabled = !value);
            }
        } 

        public bool IncrementOnLoseEnabled 
        {
            get { return _isIncrementOnLoseEnabled; }
            set { SetProperty(ref _isIncrementOnLoseEnabled, value); }
        }
        public bool IncrementOnWinEnabled
        {
			get { return _isIncrementOnWinEnabled; }
			set { SetProperty(ref _isIncrementOnWinEnabled, value); }
        }

        public bool LimitOnProfitEnabled
        {
            get { return _isLimitOnProfitEnabled; }
            set { SetProperty(ref _isLimitOnProfitEnabled, value); }
        }

		public bool LimitOnLossesEnabled
		{
			get { return _isLimitOnLossesEnabled; }
            set { SetProperty(ref _isLimitOnLossesEnabled, value); }
		}


		#endregion

		#region Methods
		

        #endregion
    }
}
