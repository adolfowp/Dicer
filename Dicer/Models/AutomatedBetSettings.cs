using System;
using System.Collections.Generic;
using System.Text;

namespace Dicer.Models
{
    class AutomatedBetSettings : BaseViewModel
    {
        #region Fields
        private decimal _BaseBet;
        private decimal _BetOdds;
        private int _Rolls;

        private BetOnEnum _BetOn;
        private decimal _StopOnProfit;
        private decimal _StopOnLoss;

        // On Win options
        private BetResultAction _BetAction_OnWin;
        private bool _ReturnToBase_OnWin;
        private bool _IncreaseBet_OnWin;
        private bool _ChangeOdds_OnWin;

        // On Lose options
        private BetResultAction _BetAction_OnLose;
        private bool _ReturnToBase_OnLose;
        private bool _IncreaseBet_OnLose;
        private bool _ChangeOdds_OnLose;
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
            set { SetProperty(ref _BetOdds, value); }
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
        public decimal StopOnProfit
        {
            get { return _StopOnProfit; }
            set { SetProperty(ref _StopOnProfit, value); }
        }
        public decimal StopOnLoss
        {
            get { return _StopOnLoss; }
            set { SetProperty(ref _StopOnLoss, value); }
        }

        public bool ReturnToBase_OnWin
        {
            get { return _ReturnToBase_OnWin; }
            set { SetProperty(ref _ReturnToBase_OnWin, value, 
                onChanged: UpdateBetActionOnWin); }
        }
        public bool IncreaseBet_OnWin
        {
            get { return _IncreaseBet_OnWin; }
            set { SetProperty(ref _IncreaseBet_OnWin, value,
                onChanged: UpdateBetActionOnWin); }
        }
        public bool ChangeOdds_OnWin
        {
            get { return _ChangeOdds_OnWin; }
            set { SetProperty(ref _ChangeOdds_OnWin, value,
                onChanged: UpdateBetActionOnWin); }
        }
        
        public bool ReturnToBase_OnLose
        {
            get { return _ReturnToBase_OnLose; }
            set { SetProperty(ref _ReturnToBase_OnLose, value,
                onChanged: UpdateBetActionOnLose); }
        }
        public bool IncreaseBet_OnLose
        {
            get { return _IncreaseBet_OnLose; }
            set { SetProperty(ref _IncreaseBet_OnLose, value,
                onChanged: UpdateBetActionOnLose); }
        }
        public bool ChangeOdds_OnLose
        {
            get { return _ChangeOdds_OnLose; }
            set { SetProperty(ref _ChangeOdds_OnLose, value,
                onChanged: UpdateBetActionOnLose); }
        }
        #endregion

        #region Methods
        void UpdateBetActionOnWin()
        {

        }

        void UpdateBetActionOnLose()
        {

        }
        #endregion
    }
}
