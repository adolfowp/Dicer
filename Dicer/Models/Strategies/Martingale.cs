using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dicer 
{
    class Martingale : IAutomationRunner
    {
        #region Fields
        bool betHigh;
        decimal odd;
        decimal chance;
        decimal multiplier;
        decimal startingBet;

        volatile bool stop = false;
        #endregion

        public Martingale(bool BetHigh, decimal Odd, decimal MultiplierOnLoss, decimal StartingBet)
        {
            betHigh = BetHigh;
            odd = Odd;
            multiplier = MultiplierOnLoss;
            chance = 100 / Odd;
            startingBet = StartingBet;
        }

        #region Methods
        public async Task<int> Run(DiceSite Site)
        {
            var currentAmount = startingBet;

            while(CanExecute(Site))
            {
                if (!await Site.PlaceBet(betHigh, currentAmount, chance))
                {
                    currentAmount *= multiplier;
                }
                else
                    currentAmount = startingBet;
                
            };

            return await Task<int>.FromResult(Site.wins - Site.losses);
        }

        public bool CanExecute(DiceSite Site)
        {
            return !stop;
        }

        public void Stop()
        {
            stop = true;
        }
        #endregion
    }
}
