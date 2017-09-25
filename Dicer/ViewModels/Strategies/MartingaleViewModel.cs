using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Dicer
{
    public class MartingaleViewModel : PlayViewModel, IAutomationRunner
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

        public MartingaleViewModel(DiceSite Site)
            : base(Site)
        {
            StartCommand = new Command(async () => 
            {
                stop = false;
                await Run(_site);  
            });

            Title = "Martingale";
        }

        #region Properties
        public decimal BaseBalance
        {
            get => _baseBalance;
            set => SetProperty(ref _baseBalance, value);
        }

        public ICommand StartCommand { get; private set; }
        public ICommand StopCommand { get; private set; }

        public decimal StartingBet
        {
            get => _startingBet;
            set => SetProperty(ref _startingBet, value);
        }
        public decimal OnLose
        {
            get => _onLose;
            set => SetProperty(ref _onLose, value);
        }
        public decimal OnWin
        {
            get => _onWin;
            set => SetProperty(ref _onWin, value);
        }

        public bool ReturnToBaseOnLose
        {
            get => _returnToBaseOnLose;
            set => SetProperty(ref _returnToBaseOnLose, value,
                               onChanged: () => IncrementOnLoseEnabled = !value);
        }

        public bool ReturnToBaseOnWin
        {
            get => _returnToBaseOnWin;
            set => SetProperty(ref _returnToBaseOnWin, value,
                               onChanged: () => IncrementOnWinEnabled = !value);
        } 

        public bool IncrementOnLoseEnabled 
        {
            get => _isIncrementOnLoseEnabled;
            set => SetProperty(ref _isIncrementOnLoseEnabled, value);
        }
        public bool IncrementOnWinEnabled
        {
			get => _isIncrementOnWinEnabled;
			set => SetProperty(ref _isIncrementOnWinEnabled, value);
        }

        public bool LimitOnProfitEnabled
        {
            get => _isLimitOnProfitEnabled;
            set => SetProperty(ref _isLimitOnProfitEnabled, value);
        }

		public bool LimitOnLossesEnabled
		{
			get => _isLimitOnLossesEnabled;
            set => SetProperty(ref _isLimitOnLossesEnabled, value);
		}


		#endregion

		#region Methods
		public async Task<int> Run(DiceSite Site)
		{
            if (LimitOnLossesEnabled && Site.profit < )
				return 0;

			if (stoponwin)
			{

			}

			return await Task<int>.FromResult(0);
		}

        #endregion
    }
}
