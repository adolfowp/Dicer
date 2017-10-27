using System;
using System.Collections.Generic;
using System.Text;

namespace Dicer.Models
{
    public class AutomatedBetSettings : BaseViewModel
    {
        #region Fields
        private decimal _BaseBet = 0.00000001m;
        private decimal _BetOdds = 2.00m;
        private decimal _Chance = 49.95m;
        private int _Rolls = 0;

        private BetOnEnum _BetOn = BetOnEnum.Alternate;
        private bool _MustBetHigh = true;
        private bool _MustBetLow;
        private bool _MustBetAlternate;

        private bool _ShouldStopOnProfit = false;
        private bool _ShouldStopOnLoss = false;

        private decimal _StopOnProfit;
        private decimal _StopOnLoss;

        // On Win options
        private bool _ReturnToBase_OnWin = true;
        private bool _IncreaseBet_OnWin;
        private bool _ChangeOdds_OnWin;

        private decimal _IncreaseAmount_OnWin;
        private decimal _NewOdd_OnWin;

        // On Lose options
        private bool _ReturnToBase_OnLose = true;
        private bool _IncreaseBet_OnLose;
        private bool _ChangeOdds_OnLose;

        private decimal _IncreaseAmount_OnLose;
        private decimal _NewOdd_OnLose;
        #endregion

        #region Properties
        public decimal BaseBet
        {
            get { return _BaseBet; }
            set { SetProperty(ref _BaseBet, value); }
        }
        public decimal BetOdds
        {
            get { return _BetOdds; }
            set
            {
                SetProperty(ref _BetOdds, value,
                    onChanged: () =>
                    {
                        SetProperty(ref _Chance, (1 / value) * 0.99m);
                    });
            }
        }

        public decimal Chance
        {
            get { return _Chance; }
            set
            {
                SetProperty(ref _Chance, value,
                    onChanged: () =>
                    {
                        SetProperty(ref _BetOdds, (1 / value) * 0.99m);
                    });
            }
        }
        public int Rolls
        {
            get { return _Rolls; }
            set { SetProperty(ref _Rolls, value); }
        }

        public BetOnEnum BetOn
        {
            get { return _BetOn; }
            set { SetProperty(ref _BetOn, value); }
        }

        public bool MustBetHigh
        {
            get { return _MustBetHigh; }
            set 
            {
                SetProperty(ref _MustBetHigh, value,
                                onChanged: () =>
                {
                    if (value)
                    {
                        MustBetLow = MustBetAlternate = false;
                        BetOn = BetOnEnum.High;
                    }
                });
            }
        }

        public bool MustBetLow
        {
            get { return _MustBetLow; }
            set
            {
                SetProperty(ref _MustBetLow, value,
                                onChanged: () =>
                {
                    if (value)
                    {
                        MustBetHigh = MustBetAlternate = false;
                        BetOn = BetOnEnum.Low;
                    }
                });
            }
        }

        public bool MustBetAlternate
        {
            get { return _MustBetAlternate; }
            set 
            {
                SetProperty(ref _MustBetAlternate, value,
                                onChanged: () =>
                {
                    if (value)
                    {
                        MustBetHigh = MustBetLow = false;
                        BetOn = BetOnEnum.Alternate;
                    }
                });
            }
        }

        public bool ShouldStopOnProfit
        {
            get { return _ShouldStopOnProfit; }
            set { SetProperty(ref _ShouldStopOnProfit, value); }
        }
        public decimal StopOnProfit
        {
            get { return _StopOnProfit; }
            set { SetProperty(ref _StopOnProfit, value); }
        }
        public bool ShouldStopOnLoss
        {
            get { return _ShouldStopOnLoss; }
            set { SetProperty(ref _ShouldStopOnLoss, value); }
        }
        public decimal StopOnLoss
        {
            get { return _StopOnLoss; }
            set { SetProperty(ref _StopOnLoss, value); }
        }

        public BetResultAction BetAction_OnWin { get; set; }

        public bool ReturnToBase_OnWin
        {
            get { return _ReturnToBase_OnWin; }
            set 
            { 
                SetProperty(ref _ReturnToBase_OnWin, value, 
                onChanged: () =>
                            UpdateBetActionOnWin(BetResultAction.ReturnToBase)); 
            }
        }
        public bool IncreaseBet_OnWin
        {
            get { return _IncreaseBet_OnWin; }
            set 
            { 
                SetProperty(ref _IncreaseBet_OnWin, value,
                onChanged: () =>
                            UpdateBetActionOnWin(BetResultAction.Increase)); 
            }
        }
        public bool ChangeOdds_OnWin
        {
            get { return _ChangeOdds_OnWin; }
            set 
            { 
                SetProperty(ref _ChangeOdds_OnWin, value,
                onChanged: () => 
                            UpdateBetActionOnWin(BetResultAction.ChangeOdds)); 
            }
        }

        public decimal IncreaseAmount_OnWin
        {
            get { return _IncreaseAmount_OnWin; }
            set { SetProperty(ref _IncreaseAmount_OnWin, value); }
        }
        public decimal NewOdd_OnWin
        {
            get { return _NewOdd_OnWin; }
            set { SetProperty(ref _NewOdd_OnWin, value); }
        }

        public BetResultAction BetAction_OnLose { get; set; }
        public bool ReturnToBase_OnLose
        {
            get { return _ReturnToBase_OnLose; }
            set 
            { 
                SetProperty(ref _ReturnToBase_OnLose, value,
                onChanged: () =>
                            UpdateBetActionOnLose(BetResultAction.ReturnToBase));
            }
        }
        public bool IncreaseBet_OnLose
        {
            get { return _IncreaseBet_OnLose; }
            set 
            { 
                SetProperty(ref _IncreaseBet_OnLose, value,
                onChanged: () => 
                              UpdateBetActionOnLose(BetResultAction.Increase)); 
            }
        }
        public bool ChangeOdds_OnLose
        {
            get { return _ChangeOdds_OnLose; }
            set 
            { 
                SetProperty(ref _ChangeOdds_OnLose, value,
                onChanged: () =>
                            UpdateBetActionOnLose(BetResultAction.ChangeOdds)); 
            }
        }

        public decimal IncreaseAmount_OnLose
        {
            get { return _IncreaseAmount_OnLose; }
            set { SetProperty(ref _IncreaseAmount_OnLose, value); }
        }
        public decimal NewOdd_OnLose
        {
            get { return _NewOdd_OnLose; }
            set { SetProperty(ref _NewOdd_OnLose, value); }
        }

        #endregion

        #region Methods
        void UpdateBetActionOnWin(BetResultAction selected)
        {
            switch (selected)
            {
                case BetResultAction.ReturnToBase:
                    if (ReturnToBase_OnWin == true)
                        IncreaseBet_OnWin = false;
                    break;
                case BetResultAction.Increase:
                    if (IncreaseBet_OnWin == true)
                        ReturnToBase_OnWin = false;
                    break;
            }

            var res = 0;

            if (ReturnToBase_OnWin) res |= (int)BetResultAction.ReturnToBase;
            else if (IncreaseBet_OnWin) res |= (int)BetResultAction.Increase;
            if (ChangeOdds_OnWin) res |= (int)BetResultAction.ChangeOdds;

            BetAction_OnWin = (BetResultAction)res;
        }

        void UpdateBetActionOnLose(BetResultAction selected)
        {
            switch (selected)
            {
                case BetResultAction.ReturnToBase:
                    if (ReturnToBase_OnLose == true)
                        IncreaseBet_OnLose = false;
                    break;
                case BetResultAction.Increase:
                    if (IncreaseBet_OnLose == true)
                        ReturnToBase_OnLose = false;
                    break;
            }

            var res = 0;

            if (ReturnToBase_OnLose) res |= (int)BetResultAction.ReturnToBase;
            else if (IncreaseBet_OnLose) res |= (int)BetResultAction.Increase;
            if (ChangeOdds_OnLose) res |= (int)BetResultAction.ChangeOdds;

            BetAction_OnLose = (BetResultAction)res;
        }
        #endregion
    }
}
