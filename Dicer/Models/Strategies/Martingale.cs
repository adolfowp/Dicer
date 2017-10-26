using Dicer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dicer 
{
    class Martingale : IAutomationRunner
    {
        #region Fields
        AutomatedBetSettings _settings;

        volatile bool stop = false;
        #endregion

        public Martingale(AutomatedBetSettings settings)
        {
            _settings = settings;
        }

        #region Methods
        public async Task<int> Run(DiceSite Site)
        {
            var currentAmount = _settings.BaseBet;
            bool bettingHigh = true;

            if (_settings.BetOn == BetOnEnum.Low)
                bettingHigh = false;

            while (CanExecute(Site))
            {
                if (!await Site.PlaceBet(bettingHigh, currentAmount, _settings.Chance))
                {
                    // perdita
                    if (_settings.BetAction_OnLose == BetResultAction.ReturnToBase)
                    {
                        currentAmount = _settings.BaseBet;
                    }
                    if (_settings.BetAction_OnLose == BetResultAction.Increase)
                    {
                        currentAmount *= (_settings.IncreaseAmount_OnLose / 100m + 1);
                    }
                    if (_settings.BetAction_OnLose == BetResultAction.ChangeOdds)
                    {
                        _settings.BetOdds = _settings.ChangeOdd_OnLose;
                    }
                }
                else
                {
                    // vincita
                    if (_settings.BetAction_OnWin == BetResultAction.ReturnToBase)
                    {
                        currentAmount = _settings.BaseBet;
                    }
                    if (_settings.BetAction_OnWin == BetResultAction.Increase)
                    {
                        currentAmount *= (_settings.IncreaseAmount_OnWin / 100m + 1);
                    }
                    if (_settings.BetAction_OnWin == BetResultAction.ChangeOdds)
                    {
                        _settings.BetOdds = _settings.ChangeOdd_OnWin;
                    }
                }

                if (_settings.BetOn == BetOnEnum.Alternate)
                    bettingHigh = !bettingHigh;

            };

            return await Task<int>.FromResult(Site.wins - Site.losses);
        }

        public bool CanExecute(DiceSite Site)
        {
            if(_settings.ShouldStopOnProfit)
            {
                // Manage stop profit
                if (Site.profit >= _settings.StopOnProfit)
                    return false;
            }
            if(_settings.ShouldStopOnLoss && Site.profit < 0)
            {
                if (Math.Abs(Site.profit) >= _settings.StopOnLoss)
                    return false;
            }

            return !stop;
        }

        public void Stop()
        {
            stop = true;
        }
        #endregion
    }
}
