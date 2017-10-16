using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Dicer
{
    public class PlayViewModel : BaseViewModel
    {
        #region Fields
        protected readonly DiceSite _site;
        decimal _betAmount = 0.00000001m;
        string _status;
        decimal _betChance = 49.5m;
        decimal _betMultiplier;
        private string _lowText;
        private string _highText;

        #region Variables
        public int logging = 0;
        protected Random rand = new Random();
        protected bool retriedbet = false;
        protected decimal StartBalance = 0;
        protected decimal Lastbet = -1;
        protected decimal MinBet = 0;
        protected decimal Multiplier = 0;
        protected decimal WinMultiplier = 0;
        protected decimal Limit = 0;

        protected decimal LargestBet = 0;
        protected decimal LargestWin = 0;
        protected decimal LargestLoss = 0;
        protected decimal LowerLimit = 0;
        protected decimal Devider = 0;
        protected decimal WinDevider = 0;
        protected decimal Chance = 0;
        protected decimal avgloss = 0;
        protected decimal avgwin = 0;
        protected decimal avgstreak = 0;
        protected decimal currentprofit = 0;
        protected decimal profit = 0;
        protected decimal luck = 0;
        protected decimal wagered = 0;
        protected int numwinstreasks = 0;
        protected int numlosesreaks = 0;
        protected int numstreaks = 0;
        protected int Wins = 0;
        protected int Losses = 0;
        protected int Winstreak = 0;
        protected int BestStreak = 0;
        protected int WorstStreak = 0;
        protected int BestStreak2 = 0;
        protected int WorstStreak2 = 0;
        protected int BestStreak3 = 0;
        protected int WorstStreak3 = 0;
        protected int Losestreak = 0;
        protected int timecounter = 0;

        protected int iMultiplyCounter = 0;
        protected int MaxMultiplies = 0;
        protected int WinMaxMultiplies = 0;
        protected int Devidecounter = 0;
        protected int WinDevidecounter = 0;
        protected int SoundStreakCount = 15;
        protected int restartcounter = 0;

        protected int laststreaklose = 0;
        protected int laststreakwin = 0;

        protected bool stop = true;
        protected bool withdraw = false;
        protected bool invest = false;
        protected bool reset = false;
        protected bool running = false;
        protected bool stoponwin = false;

        #region settings vars
        public bool tray = false;
        public bool Sound = true;
        public bool SoundWithdraw = true;
        public bool SoundLow = true;
        public bool SoundStreak = false;
        public bool autologin = false;
        public bool autostart = false;
        public string username = "";
        public string password = "";
        public string Botname = "";
        public Email Emails { get; set; }
        protected bool autoseeds = true;
        protected string ching = "";
        protected string salarm = "";
        protected bool startupMessage = true;
        protected int donateMode = 2;
        protected decimal donatePercentage = 1;

        #endregion
        #endregion
        #endregion

        public PlayViewModel(DiceSite Site)
        {
            _site = Site;
            Bets = new ObservableCollection<Bet>();

            BetHighCommand = new Command(async () =>
                                         await _site.PlaceBet(true, BetAmount, BetChance));

            BetLowCommand = new Command(async () =>
                                        await _site.PlaceBet(false, BetAmount, BetChance));

            MessagingCenter.Subscribe<DiceSite, decimal>(this, "updateBalance", (obj, val) =>
            {
                OnPropertyChanged(nameof(Amount));
            });

            MessagingCenter.Subscribe<DiceSite, Bet>(this, "addBet", (obj, bet) =>
            {
                Bets.Add(bet);
            });

            MessagingCenter.Subscribe<DiceSite, string>(this, "updateStatus", (obj, message) =>
            {
                Status = message;
            });

        }

        #region Properties
        public ICommand BetHighCommand { get; set; }
        public ICommand BetLowCommand { get; set; }

        public decimal BetAmount
        {

            get { return _betAmount; }
            set { SetProperty(ref _betAmount, value); }
        }

        public decimal BetChance
        {
            get { return _betChance; }
            set
            {
                SetProperty(ref _betChance, value,
                             onChanged: () =>
                  {
                      BetMultiplier = (100.0m / _betChance);
                      OnPropertyChanged(nameof(HighText));
                      OnPropertyChanged(nameof(LowText));
                  });
            }
        }

        public decimal BetMultiplier
        {
            get { return _betMultiplier; }
            set { SetProperty(ref _betMultiplier, value); }
        }

        public decimal MinimumChanceAllowed { get; } = 0.01m;
        public decimal MaximumChanceAllowed { get; } = 98.0m;

        public decimal Amount
        {
            get { return _site.balance; }
        }

        public string Status
        {
            get { return _status; }
            set { SetProperty(ref _status, value); }
        }

        public string Currency
        {
            get { return _site.Currency; }
            set { _site.Currency = value; }
        }

        public string[] Currencies => _site.Currencies;

        public decimal LowText
        {
            get { return _betChance; }
        }

        public decimal HighText
        {
            get { return 100m - _betChance; }
        }

        public ObservableCollection<Bet> Bets { get; set; }
        #endregion
    }
}